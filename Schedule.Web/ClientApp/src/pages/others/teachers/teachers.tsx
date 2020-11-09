import {
  Checkbox,
  Container,
  Grid,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableRow
} from '@material-ui/core';
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
import { IGetAllTeacherResponseDto } from '../../../models';
import { teachersPath } from '../../../routes';
import { deleteTeacher, getAllTeachers } from '../../../services/teacher.service';
import translations, { getErrorCodeTranslation } from '../../../services/translations';


interface State {
  isBusy: boolean;
  currentPage: number;
  totalPages: number;
  itemsPerPage: number;
  totalRecords: number;
  orderBy: keyof IGetAllTeacherResponseDto;
  orderByAsc: boolean;
  searchTerm: string;
  filteredTeachers: IGetAllTeacherResponseDto[],
  teachers: IGetAllTeacherResponseDto[]
}

function Teachers() {
  const [state, setState] = useState<State>({
    isBusy: true,
    currentPage: 1,
    totalPages: 1,
    itemsPerPage: 5,
    totalRecords: 0,
    orderBy: 'firstName',
    orderByAsc: true,
    searchTerm: '',
    filteredTeachers: [],
    teachers: []
  });
  const [selectedTeachers, setSelectedTeachers] = useState<number[]>([]);

  const { enqueueSnackbar } = useSnackbar();
  const history = useHistory();

  const refreshTeachers = async () => {
    const response = await getAllTeachers();
    if (!response.succeed) {
      setState({ ...state, isBusy: false });
      enqueueSnackbar(getErrorCodeTranslation(response.errorMessageId), { variant: 'error', });
      return;
    }
    onTeachersLoaded(response.result, state.currentPage, state.itemsPerPage, state.searchTerm, 'firstName', true);
  };

  useEffect(() => {
    refreshTeachers();
  }, []);

  const onTeachersLoaded = useCallback((
    teachers: IGetAllTeacherResponseDto[],
    currentPage: number,
    itemsPerPage: number,
    searchTerm: string,
    orderBy: keyof IGetAllTeacherResponseDto,
    orderByAsc: boolean) => {
    let t = teachers;
    switch (orderBy) {
      case 'identifierNumber':
        t = orderByAsc
          ? t.sort((x, y) => x.identifierNumber - y.identifierNumber)
          : t.sort((x, y) => y.identifierNumber - x.identifierNumber);
        break;
      case 'priority':
        t = orderByAsc
          ? t.sort((x, y) => x.priority.localeCompare(y.priority))
          : t.sort((x, y) => y.priority.localeCompare(x.priority));
        break;
      case 'firstName':
        t = orderByAsc ? t.sort((x, y) => {
          const a = `${x.firstName} ${x.firstLastName}`;
          const b = `${y.firstName} ${y.firstLastName}`;
          return a.localeCompare(b);
        }) : t.sort((x, y) => {
          const a = `${x.firstName} ${x.firstLastName}`;
          const b = `${y.firstName} ${y.firstLastName}`;
          return b.localeCompare(a);
        });
        break;
      default:
        break;
    }

    const isFiltering = !validator.isEmpty(searchTerm);
    t = teachers.filter(t => {
      const fullname = `${t.firstName} ${t.firstLastName}`;
      if (isFiltering)
        return fullname.toLowerCase().includes(searchTerm.toLowerCase()) || `${t.identifierNumber}`.includes(searchTerm);
      return true;
    });

    const totalRecords = isFiltering ? t.length : teachers.length;
    const totalPages = Math.ceil(totalRecords / itemsPerPage);
    const page = currentPage > totalPages ? 1 : currentPage;
    const skip = itemsPerPage * (page - 1);

    t = t.slice(skip, itemsPerPage + skip);

    setState({
      ...state,
      isBusy: false,
      searchTerm: searchTerm,
      filteredTeachers: t,
      teachers: teachers,
      currentPage: page,
      itemsPerPage: itemsPerPage,
      totalPages: totalPages,
      totalRecords: totalRecords,
      orderBy: orderBy,
      orderByAsc: orderByAsc
    });
  }, []);

  const teacherSelectionChanged = (id: number, isSelected: boolean) => {
    const newValues: number[] = !isSelected
      ? selectedTeachers.filter(el => el !== id)
      : selectedTeachers.concat(id);
    setSelectedTeachers(newValues);
  };

  const sortDirectionChanged = (orderBy: keyof IGetAllTeacherResponseDto, orderByAsc: boolean) => {
    onTeachersLoaded(state.teachers, state.currentPage, state.itemsPerPage, state.searchTerm, orderBy, orderByAsc);
  };

  const itemsPerPageChanged = (newVal: number) => {
    onTeachersLoaded(state.teachers, state.currentPage, newVal, state.searchTerm, state.orderBy, state.orderByAsc);
  };

  const searchTermChanged = (newVal: string) => {
    onTeachersLoaded(state.teachers, state.currentPage, state.itemsPerPage, newVal, state.orderBy, state.orderByAsc);
  }

  const pageChanged = (newVal: number) => {
    onTeachersLoaded(state.teachers, newVal, state.itemsPerPage, state.searchTerm, state.orderBy, state.orderByAsc);
  };

  const onEditClick = useCallback(() => {
    if (selectedTeachers.length === 0)
      return;

    const id = selectedTeachers[0];
    const path = `${teachersPath}/${id}`;
    history.push(path);
  }, [history, selectedTeachers, teachersPath]);

  const onDeleteClick = async () => {
    if (selectedTeachers.length === 0)
      return;

    setState({ ...state, isBusy: true });

    let notDeleted = 0;
    for (let index = 0; index < selectedTeachers.length; index++) {
      const id = selectedTeachers[index];
      const response = await deleteTeacher(id);
      if (!response.succeed) {
        console.log(response);
        notDeleted++;
        enqueueSnackbar(getErrorCodeTranslation(response.errorMessageId), { variant: 'error' });
      }
    }

    if (notDeleted === 0) {
      const msg = String.Format(translations.xItemsWereDeleted, selectedTeachers.length, translations.subjects);
      enqueueSnackbar(msg, { variant: 'success' });
    }

    setSelectedTeachers([]);
    await refreshTeachers();
  };

  const onFabClick = () => {
    const path = `${teachersPath}/0`;
    history.push(path);
  };

  const headerCells: Header<IGetAllTeacherResponseDto>[] = [
    {
      isOrderable: false,
      text: '',
    },
    {
      isOrderable: true,
      text: translations.identifierNumber,
      orderByKey: 'identifierNumber'
    },
    {
      isOrderable: true,
      text: translations.name,
      orderByKey: 'firstName'
    },
    {
      isOrderable: true,
      text: translations.priority,
      orderByKey: 'priority'
    },
  ];

  const rows = state.filteredTeachers.map(t => {
    const isSelected = selectedTeachers.includes(t.id);
    const fullname = `${t.firstName} ${t.firstLastName}`;
    return <TableRow key={t.id}
      onClick={() => teacherSelectionChanged(t.id, !isSelected)}
      style={{ cursor: 'pointer' }}>
      <TableCell padding="checkbox">
        <Checkbox checked={isSelected} onChange={(e, isChecked) => teacherSelectionChanged(t.id, isChecked)} />
      </TableCell>
      <TableCell align="center">{t.identifierNumber}</TableCell>
      <TableCell align="center">{fullname}</TableCell>
      <TableCell align="center">{t.priority}</TableCell>
    </TableRow>;
  });

  return <Container>
    <PageTitle title={translations.teachers} showLoading={state.isBusy} />
    <CustomTableToolbar
      deleteDialogTitle={translations.deleteSubjects}
      selectedNumberOfItems={selectedTeachers.length}
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

export default Teachers;
