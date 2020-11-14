import { Container, Grid } from '@material-ui/core';
import { useSnackbar } from 'notistack';
import React, { useCallback, useEffect, useState } from 'react';
import { useHistory } from 'react-router-dom';
import validator from 'validator';
import CareerCard from '../../../components/career/career-card';
import CustomTableSearch from '../../../components/custom-table/custom-table-search';
import CustomFab from '../../../components/others/custom-fab';
import PageTitle from '../../../components/page-title/page-title';
import { IGetAllCareersResponseDto } from '../../../models';
import { careersPath } from '../../../routes';
import { deleteCareer, getAllCareers } from '../../../services/career.service';
import translations, { getErrorCodeTranslation } from '../../../services/translations';

interface State {
    isBusy: boolean;
    totalRecords: number;
    orderBy: keyof IGetAllCareersResponseDto;
    orderByAsc: boolean;
    searchTerm: string;
    filteredCareers: IGetAllCareersResponseDto[],
    careers: IGetAllCareersResponseDto[]
}

function Careers() {
    const [state, setState] = useState<State>({
        isBusy: true,
        totalRecords: 0,
        orderBy: 'id',
        orderByAsc: true,
        searchTerm: '',
        filteredCareers: [],
        careers: []
    });

    const { enqueueSnackbar } = useSnackbar();
    const history = useHistory();

    const onCareersLoaded = useCallback((
        careers: IGetAllCareersResponseDto[],
        searchTerm: string,
        orderBy: keyof IGetAllCareersResponseDto,
        orderByAsc: boolean) => {
        let c = careers;
        switch (orderBy) {
            case 'id':
                c = orderByAsc
                    ? c.sort((x, y) => x.id - y.id)
                    : c.sort((x, y) => y.id - x.id);
                break;
            case 'name':
                c = orderByAsc
                    ? c.sort((x, y) => x.name.localeCompare(y.name))
                    : c.sort((x, y) => y.name.localeCompare(x.name));
                break;
            default:
                break;
        }

        const isFiltering = !validator.isEmpty(searchTerm);
        c = careers.filter(x => {
            if (isFiltering)
                return x.name.toLowerCase().includes(searchTerm.toLowerCase());
            return true;
        });

        const totalRecords = isFiltering ? c.length : careers.length;

        setState(s => ({
            ...s,
            isBusy: false,
            searchTerm: searchTerm,
            filteredCareers: c,
            careers: careers,
            totalRecords: totalRecords,
            orderBy: orderBy,
            orderByAsc: orderByAsc
        }));
    }, []);

    const refreshCareers = useCallback(async () => {
        const response = await getAllCareers();
        if (!response.succeed) {
            setState(s => ({ ...s, isBusy: false }));
            enqueueSnackbar(getErrorCodeTranslation(response.errorMessageId), { variant: 'error', });
            return;
        }
        onCareersLoaded(response.result, '', 'name', true);
    }, [onCareersLoaded, enqueueSnackbar]);

    useEffect(() => {
        refreshCareers();
    }, [refreshCareers]);

    const searchTermChanged = (newVal: string) => {
        onCareersLoaded(state.careers, newVal, state.orderBy, state.orderByAsc);
    }

    const onDeleteClick = async (id: number) => {
        setState({ ...state, isBusy: true });
        const response = await deleteCareer(id);
        let careers = state.careers;
        if (response.succeed) {
            careers = careers.filter(t => t.id !== id);
            enqueueSnackbar(translations.itemWasDeleted, { variant: 'success' });
        } else {
            enqueueSnackbar(getErrorCodeTranslation(response.errorMessageId), { variant: 'error' });
        }

        onCareersLoaded(careers, state.searchTerm, state.orderBy, state.orderByAsc);
    };

    const onFabClick = () => {
        const path = `${careersPath}/0`;
        history.push(path);
    };

    const cards = state.filteredCareers.map(x => {
        return <Grid key={x.id} item sm={4} style={{ width: '100%' }}>
            <CareerCard {...x} onDelete={onDeleteClick} isBusy={state.isBusy} />
        </Grid>;
    });

    return <Container>
        <PageTitle title={translations.careers} showLoading={state.isBusy} />
        <CustomTableSearch
            showSearch
            showItemsPerPage={false}
            searchText={state.searchTerm}
            isBusy={state.isBusy}
            onSearchTermChanged={searchTermChanged} />
        <Grid container justify="center" direction="row" spacing={5}>
            {cards}
        </Grid>
        <CustomFab onClick={onFabClick} />
    </Container>;
}

export default Careers;