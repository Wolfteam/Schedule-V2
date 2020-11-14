import { Button, Container, Grid } from '@material-ui/core';
import { useSnackbar } from 'notistack';
import React, { useCallback, useEffect, useState } from 'react';
import { useHistory } from 'react-router-dom';
import ClassroomCard from '../../../components/classroom/classroom-card';
import CustomTableSearch from '../../../components/custom-table/custom-table-search';
import CustomFab from '../../../components/others/custom-fab';
import PageTitle from '../../../components/page-title/page-title';
import { buildPaginatedRequest, IGetAllClassroomsResponseDto, IPaginatedRequestDto } from '../../../models';
import { classRoomsPath } from '../../../routes';
import { deleteClassroom, getAllClassrooms } from '../../../services/classroom.service';
import translations, { getErrorCodeTranslation } from '../../../services/translations';

interface State {
    isBusy: boolean;
    currentPage: number;
    totalPages: number;
    itemsPerPage: number;
    totalRecords: number;
    orderBy: keyof IGetAllClassroomsResponseDto;
    orderByAsc: boolean;
    searchTerm: string;
    canLoadMore: boolean;
    classrooms: IGetAllClassroomsResponseDto[];
}

function Classrooms() {
    const [state, setState] = useState<State>({
        isBusy: true,
        currentPage: 1,
        totalPages: 1,
        itemsPerPage: 5,
        totalRecords: 0,
        orderBy: 'name',
        orderByAsc: true,
        searchTerm: '',
        canLoadMore: false,
        classrooms: [],
    });

    const { enqueueSnackbar } = useSnackbar();

    const history = useHistory();

    const refreshClassrooms = useCallback(async (request: IPaginatedRequestDto) => {
        const response = await getAllClassrooms(request);
        if (!response.succeed) {
            enqueueSnackbar(getErrorCodeTranslation(response.errorMessageId), { variant: 'error' });
            setState(s => ({ ...s, isBusy: false }));
            return;
        }

        const hasReachedMax = response.result.length === 0 || response.result.length < response.take || response.totalPages === 1;
        setState(s => ({
            ...s,
            isBusy: false,
            canLoadMore: !hasReachedMax,
            currentPage: response.currentPage,
            itemsPerPage: response.take,
            classrooms: s.classrooms.concat(response.result),
            totalPages: response.totalPages,
            totalRecords: response.totalRecords
        }));
    }, [enqueueSnackbar]);

    useEffect(() => {
        const request = buildPaginatedRequest(state.currentPage, state.itemsPerPage, state.searchTerm, state.orderBy, state.orderByAsc);
        refreshClassrooms(request);
    }, [state.currentPage, state.itemsPerPage, state.searchTerm, state.orderBy, state.orderByAsc, refreshClassrooms]);

    const searchTermChanged = (newVal: string) => {
        setState(s => ({ ...s, isBusy: true, searchTerm: newVal, currentPage: 1, classrooms: [] }));
    };

    const onDeleteClick = async (id: number) => {
        setState({ ...state, isBusy: true });
        const response = await deleteClassroom(id);
        if (response.succeed) {
            enqueueSnackbar(translations.itemWasDeleted, { variant: 'success' });
        } else {
            enqueueSnackbar(getErrorCodeTranslation(response.errorMessageId), { variant: 'error' });
        }
        const request = buildPaginatedRequest(state.currentPage, state.itemsPerPage, state.searchTerm, state.orderBy, state.orderByAsc);
        await refreshClassrooms(request);
    };

    const onFabClick = () => {
        const path = `${classRoomsPath}/0`;
        history.push(path);
    };

    const onLoadMoreClick = async () => {
        setState({ ...state, isBusy: true, currentPage: state.currentPage + 1 });
    };

    const cards = state.classrooms.map(x => {
        return <Grid key={x.id} item sm={4} style={{ width: '100%' }}>
            <ClassroomCard {...x} isBusy={state.isBusy} onDelete={onDeleteClick} />
        </Grid>;
    });

    if (state.canLoadMore) {
        const item = <Grid key="load-more" style={{ margin: 'auto' }}>
            <Button disabled={state.isBusy} onClick={onLoadMoreClick}>Load More</Button>
        </Grid>
        cards.push(item);
    }

    return <Container>
        <PageTitle title={translations.classrooms} showLoading={state.isBusy} />
        <CustomTableSearch
            showSearch
            showItemsPerPage={false}
            searchText={state.searchTerm}
            isBusy={state.isBusy}
            onSearchTermChanged={searchTermChanged} />
        <Grid container justify="center" direction="row" alignItems="center" spacing={5}>
            {cards}
        </Grid>
        <CustomFab onClick={onFabClick} />
    </Container>;
}

export default Classrooms;