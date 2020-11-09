import { Checkbox, Container, Grid, Table, TableBody, TableCell, TableContainer, TableRow } from '@material-ui/core';
import { useSnackbar } from 'notistack';
import React, { useCallback, useEffect, useState } from 'react';
import { useHistory } from 'react-router-dom';
import { String } from 'typescript-string-operations';
import CustomTableHeader, { Header } from '../../../components/custom-table/custom-table-header';
import CustomTablePagination from '../../../components/custom-table/custom-table-pagination';
import CustomTableSearch from '../../../components/custom-table/custom-table-search';
import CustomTableToolbar from '../../../components/custom-table/custom-table-toolbar';
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
    classrooms: IGetAllClassroomsResponseDto[];
    searchTimeout: number;
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
        classrooms: [],
        searchTimeout: 0
    });

    const [selectedClassrooms, setSelectedClassrooms] = useState<number[]>([]);

    const { enqueueSnackbar } = useSnackbar();

    const history = useHistory();

    const refreshClassrooms = async (request: IPaginatedRequestDto) => {
        const response = await getAllClassrooms(request);
        if (!response.succeed) {
            enqueueSnackbar(getErrorCodeTranslation(response.errorMessageId), { variant: 'error' });
            setState({ ...state, isBusy: false });
            return;
        }
        setState({
            ...state,
            isBusy: false,
            currentPage: response.currentPage,
            itemsPerPage: response.take,
            classrooms: response.result,
            totalPages: response.totalPages,
            totalRecords: response.totalRecords
        });
    };

    useEffect(() => {
        const request = buildPaginatedRequest(state.currentPage, state.itemsPerPage, state.searchTerm, state.orderBy, state.orderByAsc);
        refreshClassrooms(request);
    }, [state.currentPage, state.itemsPerPage, state.searchTerm, state.orderBy, state.orderByAsc]);

    const sortDirectionChanged = useCallback((orderBy: keyof IGetAllClassroomsResponseDto, orderByAsc: boolean) => {
        setState({ ...state, isBusy: true, orderBy: orderBy, orderByAsc: orderByAsc });
    }, []);

    const itemsPerPageChanged = useCallback((newVal: number) => {
        setState({ ...state, isBusy: true, itemsPerPage: newVal });
    }, []);

    const searchTermChanged = useCallback((newVal: string) => {
        setState({ ...state, isBusy: true, searchTerm: newVal });
    }, []);

    const pageChanged = (newVal: number) => {
        setState({ ...state, isBusy: true, currentPage: newVal });
    };

    const classroomSelectionChanged = (id: number, isSelected: boolean) => {
        let newValues: number[] = [];
        if (!isSelected) {
            newValues = selectedClassrooms.filter(el => el !== id);
        } else {
            newValues = selectedClassrooms.concat(id);
        }
        console.log(newValues);
        setSelectedClassrooms(newValues);
    };

    const onEditClick = useCallback(() => {
        if (selectedClassrooms.length === 0)
            return;

        const id = selectedClassrooms[0];
        const path = `${classRoomsPath}/${id}`;
        history.push(path);
    }, [history, selectedClassrooms, classRoomsPath]);

    const onDeleteClick = async () => {
        if (selectedClassrooms.length === 0)
            return;

        setState({ ...state, isBusy: true });

        let notDeleted = 0;
        for (let index = 0; index < selectedClassrooms.length; index++) {
            const id = selectedClassrooms[index];
            const response = await deleteClassroom(id);
            if (!response.succeed) {
                console.log(response);
                notDeleted++;
                enqueueSnackbar(getErrorCodeTranslation(response.errorMessageId), { variant: 'error' });
            }
        }

        if (notDeleted === 0) {
            const msg = String.Format(translations.xItemsWereDeleted, selectedClassrooms.length, translations.classrooms);
            enqueueSnackbar(msg, { variant: 'success' });
        }

        setSelectedClassrooms([]);
        const request = buildPaginatedRequest(state.currentPage, state.itemsPerPage, state.searchTerm, state.orderBy, state.orderByAsc);
        await refreshClassrooms(request);
    };

    const onFabClick = () => {
        const path = `${classRoomsPath}/0`;
        history.push(path);
    };

    const headerCells: Header<IGetAllClassroomsResponseDto>[] = [
        {
            text: '',
            isOrderable: false
        },
        {
            text: translations.name,
            isOrderable: true,
            orderByKey: 'name',
        },
        {
            text: translations.capacity,
            isOrderable: true,
            orderByKey: 'capacity',
        },
        {
            text: translations.classroomType,
            isOrderable: true,
            orderByKey: 'classroomSubject',
        },
        {
            text: translations.createdAt,
            isOrderable: true,
            orderByKey: 'createdAt',
        },
        {
            text: translations.createdBy,
            isOrderable: true,
            orderByKey: 'createdBy',
        },
    ];


    const rows = state.classrooms.map(x => {
        const isSelected = selectedClassrooms.includes(x.id);
        const date = new Date(x.createdAt).toLocaleDateString('es-us', {
            year: 'numeric',
            month: '2-digit',
            day: 'numeric',
            hour: '2-digit',
            minute: '2-digit',
            second: '2-digit',
            timeZone: 'UTC'
        });
        return <TableRow key={x.id}
            onClick={() => classroomSelectionChanged(x.id, !isSelected)}
            style={{ cursor: 'pointer' }}>
            <TableCell padding="checkbox">
                <Checkbox checked={isSelected} onChange={(e, isChecked) => classroomSelectionChanged(x.id, isChecked)} />
            </TableCell>
            <TableCell align="center">{x.name}</TableCell>
            <TableCell align="center">{x.capacity}</TableCell>
            <TableCell align="center">{x.classroomSubject}</TableCell>
            <TableCell align="center">{date}</TableCell>
            <TableCell align="center">{x.createdBy}</TableCell>
        </TableRow>;
    });

    const deleteDialogTitle = String.Format(translations.deleteX, translations.classrooms.toLowerCase());

    return <Container>
        <PageTitle title={translations.classrooms} showLoading={state.isBusy} />
        <CustomTableToolbar
            deleteDialogTitle={deleteDialogTitle}
            selectedNumberOfItems={selectedClassrooms.length}
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

export default Classrooms;