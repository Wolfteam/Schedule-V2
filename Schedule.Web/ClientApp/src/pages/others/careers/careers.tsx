import { Checkbox, Container, Grid, Table, TableBody, TableCell, TableContainer, TableRow } from '@material-ui/core';
import { useSnackbar } from 'notistack';
import React, { useCallback, useEffect, useState } from 'react';
import { useHistory } from 'react-router-dom';
import { String } from 'typescript-string-operations';
import validator from 'validator';
import CustomTableHeader, { Header } from '../../../components/custom-table/custom-table-header';
import CustomTablePagination from '../../../components/custom-table/custom-table-pagination';
import CustomTableSearch from '../../../components/custom-table/custom-table-search';
import CustomTableToolbar from '../../../components/custom-table/custom-table-toolbar';
import CustomFab from '../../../components/others/custom-fab';
import PageTitle from '../../../components/page-title/page-title';
import { IGetAllCareersResponseDto } from '../../../models';
import { careersPath } from '../../../routes';
import { deleteCareer, getAllCareers } from '../../../services/career.service';
import translations, { getErrorCodeTranslation } from '../../../services/translations';

interface State {
    isBusy: boolean;
    currentPage: number;
    totalPages: number;
    itemsPerPage: number;
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
        currentPage: 1,
        totalPages: 1,
        itemsPerPage: 5,
        totalRecords: 0,
        orderBy: 'id',
        orderByAsc: true,
        searchTerm: '',
        filteredCareers: [],
        careers: []
    });
    const [selectedCareers, setSelectedCareers] = useState<number[]>([]);

    const { enqueueSnackbar } = useSnackbar();
    const history = useHistory();

    const onCareersLoaded = useCallback((
        careers: IGetAllCareersResponseDto[],
        currentPage: number,
        itemsPerPage: number,
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
        const totalPages = Math.ceil(totalRecords / itemsPerPage);
        const page = currentPage > totalPages ? 1 : currentPage;
        const skip = itemsPerPage * (page - 1);

        c = c.slice(skip, itemsPerPage + skip);

        setState({
            ...state,
            isBusy: false,
            searchTerm: searchTerm,
            filteredCareers: c,
            careers: careers,
            currentPage: page,
            itemsPerPage: itemsPerPage,
            totalPages: totalPages,
            totalRecords: totalRecords,
            orderBy: orderBy,
            orderByAsc: orderByAsc
        });
    }, []);

    const refreshCareers = async () => {
        const response = await getAllCareers();
        if (!response.succeed) {
            setState({ ...state, isBusy: false });
            enqueueSnackbar(getErrorCodeTranslation(response.errorMessageId), { variant: 'error', });
            return;
        }
        onCareersLoaded(response.result, state.currentPage, state.itemsPerPage, state.searchTerm, 'name', true);
    };

    useEffect(() => {
        refreshCareers();
    }, []);

    const careerSelectionChanged = (id: number, isSelected: boolean) => {
        const newValues: number[] = !isSelected
            ? selectedCareers.filter(el => el !== id)
            : selectedCareers.concat(id);
        setSelectedCareers(newValues);
    };


    const sortDirectionChanged = (orderBy: keyof IGetAllCareersResponseDto, orderByAsc: boolean) => {
        onCareersLoaded(state.careers, state.currentPage, state.itemsPerPage, state.searchTerm, orderBy, orderByAsc);
    };

    const itemsPerPageChanged = (newVal: number) => {
        onCareersLoaded(state.careers, state.currentPage, newVal, state.searchTerm, state.orderBy, state.orderByAsc);
    };

    const searchTermChanged = (newVal: string) => {
        onCareersLoaded(state.careers, state.currentPage, state.itemsPerPage, newVal, state.orderBy, state.orderByAsc);
    }

    const pageChanged = (newVal: number) => {
        onCareersLoaded(state.careers, newVal, state.itemsPerPage, state.searchTerm, state.orderBy, state.orderByAsc);
    };

    const onEditClick = useCallback(() => {
        if (selectedCareers.length === 0)
            return;

        const id = selectedCareers[0];
        const path = `${careersPath}/${id}`;
        history.push(path);
    }, [history, selectedCareers, careersPath]);

    const onDeleteClick = async () => {
        if (selectedCareers.length === 0)
            return;

        setState({ ...state, isBusy: true });

        let subjectsNotDeleted = 0;
        for (let index = 0; index < selectedCareers.length; index++) {
            const id = selectedCareers[index];
            const response = await deleteCareer(id);
            if (!response.succeed) {
                console.log(response);
                subjectsNotDeleted++;
                enqueueSnackbar(getErrorCodeTranslation(response.errorMessageId), { variant: 'error' });
            }
        }

        if (subjectsNotDeleted === 0) {
            const msg = String.Format(translations.xItemsWereDeleted, selectedCareers.length, translations.careers);
            enqueueSnackbar(msg, { variant: 'success' });
        }

        setSelectedCareers([]);
        await refreshCareers();
    };

    const onFabClick = () => {
        const path = `${careersPath}/0`;
        history.push(path);
    };

    const headerCells: Header<IGetAllCareersResponseDto>[] = [
        {
            isOrderable: false,
            text: '',
        },
        {
            isOrderable: true,
            text: 'Id',
            orderByKey: 'id'
        },
        {
            isOrderable: true,
            text: translations.name,
            orderByKey: 'name'
        }
    ];

    const rows = state.filteredCareers.map(t => {
        const isSelected = selectedCareers.includes(t.id);
        return <TableRow key={t.id}
            onClick={() => careerSelectionChanged(t.id, !isSelected)}
            style={{ cursor: 'pointer' }}>
            <TableCell padding="checkbox">
                <Checkbox checked={isSelected} onChange={(e, isChecked) => careerSelectionChanged(t.id, isChecked)} />
            </TableCell>
            <TableCell align="center">{t.id}</TableCell>
            <TableCell align="center">{t.name}</TableCell>
        </TableRow>;
    });

    return <Container>
        <PageTitle title={translations.careers} showLoading={state.isBusy} />
        <CustomTableToolbar
            deleteDialogTitle={translations.deleteCareers}
            selectedNumberOfItems={selectedCareers.length}
            onDeleteClick={onDeleteClick}
            onEditClick={onEditClick} />
        <CustomTableSearch
            searchText={state.searchTerm}
            isBusy={state.isBusy}
            onItemsPerPageChanged={itemsPerPageChanged}
            onSearchTermChanged={searchTermChanged} />
        <Grid container justify="center" direction="column">
            <Grid item xs>
                <TableContainer>
                    <Table size="small">
                        <CustomTableHeader
                            cells={headerCells}
                            orderBy={state.orderBy}
                            orderByAsc={state.orderByAsc}
                            onOrderByChanged={sortDirectionChanged} />
                        <TableBody>
                            {rows}
                        </TableBody>
                    </Table>
                </TableContainer>
            </Grid>
        </Grid>
        <CustomTablePagination
            isBusy={state.isBusy}
            currentPage={state.currentPage}
            totalPages={state.totalPages}
            totalRecords={state.totalRecords}
            onPageChanged={pageChanged} />
        <CustomFab onClick={onFabClick} />
    </Container>;
}

export default Careers;