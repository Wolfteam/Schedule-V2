import {
  Container,
  Grid
} from '@material-ui/core';
import { useSnackbar } from 'notistack';
import React, { useCallback, useEffect, useState } from 'react';
import { useHistory } from 'react-router-dom';
import validator from 'validator';
import CustomTableSearch from '../../../components/custom-table/custom-table-search';
import CustomFab from '../../../components/others/custom-fab';
import PageTitle from '../../../components/page-title/page-title';
import TeacherCard from '../../../components/teacher/teacher-card';
import { IGetAllTeacherResponseDto } from '../../../models';
import { teachersPath } from '../../../routes';
import { deleteTeacher, getAllTeachers } from '../../../services/teacher.service';
import translations, { getErrorCodeTranslation } from '../../../services/translations';

interface State {
  isBusy: boolean;
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
    totalRecords: 0,
    orderBy: 'firstName',
    orderByAsc: true,
    searchTerm: '',
    filteredTeachers: [],
    teachers: []
  });

  const { enqueueSnackbar } = useSnackbar();
  const history = useHistory();

  const onTeachersLoaded = useCallback((
    teachers: IGetAllTeacherResponseDto[],
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

    setState(s => ({
      ...s,
      isBusy: false,
      searchTerm: searchTerm,
      filteredTeachers: t,
      teachers: teachers,
      totalRecords: totalRecords,
      orderBy: orderBy,
      orderByAsc: orderByAsc
    }));
  }, []);

  const refreshTeachers = useCallback(async () => {
    const response = await getAllTeachers();
    if (!response.succeed) {
      setState(s => ({ ...s, isBusy: false }));
      enqueueSnackbar(getErrorCodeTranslation(response.errorMessageId), { variant: 'error', });
      return;
    }
    onTeachersLoaded(response.result, '', 'firstName', true);
  }, [onTeachersLoaded, enqueueSnackbar]);

  useEffect(() => {
    refreshTeachers();
  }, [refreshTeachers]);

  const searchTermChanged = (newVal: string) => {
    onTeachersLoaded(state.teachers, newVal, state.orderBy, state.orderByAsc);
  }

  const onDeleteClick = async (id: number) => {
    setState({ ...state, isBusy: true });
    const response = await deleteTeacher(id);
    let teachers = state.teachers;
    if (response.succeed) {
      teachers = teachers.filter(t => t.id !== id);
      enqueueSnackbar(translations.itemWasDeleted, { variant: 'success' });
    } else {
      enqueueSnackbar(getErrorCodeTranslation(response.errorMessageId), { variant: 'error' });
    }

    onTeachersLoaded(teachers, state.searchTerm, state.orderBy, state.orderByAsc);
  };

  const onFabClick = () => {
    const path = `${teachersPath}/0`;
    history.push(path);
  };

  const cards = state.filteredTeachers.map(x => {
    return <Grid key={x.id} item sm={4} style={{ width: '100%' }}>
      <TeacherCard {...x} onDelete={onDeleteClick} isBusy={state.isBusy} />
    </Grid>;
  });

  return <Container>
    <PageTitle title={translations.teachers} showLoading={state.isBusy} />
    <CustomTableSearch
      showSearch
      searchText={state.searchTerm}
      showItemsPerPage={false}
      isBusy={state.isBusy}
      onSearchTermChanged={searchTermChanged} />
    <Grid container justify="center" direction="row" spacing={5}>
      {cards}
    </Grid>
    <CustomFab onClick={onFabClick} />
  </Container>;
}

export default Teachers;
