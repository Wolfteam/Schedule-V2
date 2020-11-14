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
import CustomTableHeader, { Header } from '../../../components/custom-table/custom-table-header';
import CustomTablePagination from '../../../components/custom-table/custom-table-pagination';
import CustomTableSearch from '../../../components/custom-table/custom-table-search';
import CustomTableToolbar from '../../../components/custom-table/custom-table-toolbar';
import CustomFab from '../../../components/others/custom-fab';
import PageTitle from '../../../components/page-title/page-title';
import { buildPaginatedRequest, IGetAllSubjectResponseDto, IPaginatedRequestDto } from '../../../models';
import { subjectsPath } from '../../../routes';
import { deleteSubject, getAllSubjects } from '../../../services/subject.service';
import translations, { getErrorCodeTranslation } from '../../../services/translations';

interface State {
  isBusy: boolean;
  currentPage: number;
  totalPages: number;
  itemsPerPage: number;
  totalRecords: number;
  orderBy: keyof IGetAllSubjectResponseDto;
  orderByAsc: boolean;
  searchTerm: string;
  subjects: IGetAllSubjectResponseDto[];
  searchTimeout: number;
}

function Subjects() {
  const [state, setState] = useState<State>({
    isBusy: true,
    currentPage: 1,
    totalPages: 1,
    itemsPerPage: 5,
    totalRecords: 0,
    orderBy: 'code',
    orderByAsc: true,
    searchTerm: '',
    subjects: [],
    searchTimeout: 0
  });

  const [selectedSubjects, setSelectedSubjects] = useState<number[]>([]);

  const { enqueueSnackbar } = useSnackbar();

  const history = useHistory();

  const refreshSubjects = useCallback(async (request: IPaginatedRequestDto) => {
    const response = await getAllSubjects(request);
    if (!response.succeed) {
      enqueueSnackbar(getErrorCodeTranslation(response.errorMessageId), { variant: 'error' });
      setState(s => ({ ...s, isBusy: false }));
      return;
    }
    setState(s => ({
      ...s,
      isBusy: false,
      currentPage: response.currentPage,
      itemsPerPage: response.take,
      subjects: response.result,
      totalPages: response.totalPages,
      totalRecords: response.totalRecords
    }));
  }, [enqueueSnackbar]);

  useEffect(() => {
    const request = buildPaginatedRequest(state.currentPage, state.itemsPerPage, state.searchTerm, state.orderBy, state.orderByAsc);
    refreshSubjects(request);
  }, [state.currentPage, state.itemsPerPage, state.searchTerm, state.orderBy, state.orderByAsc, refreshSubjects]);

  const sortDirectionChanged = useCallback((orderBy: keyof IGetAllSubjectResponseDto, orderByAsc: boolean) => {
    setState(s => ({ ...s, isBusy: true, orderBy: orderBy, orderByAsc: orderByAsc }));
  }, []);

  const itemsPerPageChanged = useCallback((newVal: number) => {
    setState(s => ({ ...s, isBusy: true, itemsPerPage: newVal }));
  }, []);

  const searchTermChanged = useCallback((newVal: string) => {
    setState(s => ({ ...s, isBusy: true, searchTerm: newVal }));
  }, []);

  const pageChanged = (newVal: number) => {
    setState(s => ({ ...s, isBusy: true, currentPage: newVal }));
  };

  const subjectSelectionChanged = (id: number, isSelected: boolean) => {
    let newValues: number[] = [];
    if (!isSelected) {
      newValues = selectedSubjects.filter(el => el !== id);
    } else {
      newValues = selectedSubjects.concat(id);
    }
    setSelectedSubjects(newValues);
  };

  const onEditClick = useCallback(() => {
    if (selectedSubjects.length === 0)
      return;

    const id = selectedSubjects[0];
    const path = `${subjectsPath}/${id}`;
    history.push(path);
  }, [history, selectedSubjects]);

  const onDeleteClick = async () => {
    if (selectedSubjects.length === 0)
      return;

    setState({ ...state, isBusy: true });

    let notDeleted = 0;
    for (let index = 0; index < selectedSubjects.length; index++) {
      const id = selectedSubjects[index];
      const response = await deleteSubject(id);
      if (!response.succeed) {
        console.log(response);
        notDeleted++;
        enqueueSnackbar(getErrorCodeTranslation(response.errorMessageId), { variant: 'error' });
      }
    }

    if (notDeleted === 0) {
      const msg = String.Format(translations.xItemsWereDeleted, selectedSubjects.length, translations.subjects);
      enqueueSnackbar(msg, { variant: 'success' });
    }

    setSelectedSubjects([]);
    const request = buildPaginatedRequest(state.currentPage, state.itemsPerPage, state.searchTerm, state.orderBy, state.orderByAsc);
    await refreshSubjects(request);
  };

  const onFabClick = () => {
    const path = `${subjectsPath}/0`;
    history.push(path);
  };

  const headerCells: Header<IGetAllSubjectResponseDto>[] = [
    {
      text: '',
      isOrderable: false
    },
    {
      orderByKey: 'code',
      text: translations.code,
      isOrderable: true
    },
    {
      orderByKey: 'name',
      text: translations.name,
      isOrderable: true
    },
    {
      orderByKey: 'semester',
      text: translations.semester,
      isOrderable: true
    },
    {
      orderByKey: 'career',
      text: translations.career,
      isOrderable: true
    },
    {
      orderByKey: 'classroomType',
      text: translations.subjectType,
      isOrderable: true
    },
    {
      orderByKey: 'academicHoursPerWeek',
      text: translations.academicHoursPerWeeek,
      isOrderable: true
    },
    {
      orderByKey: 'totalAcademicHours',
      text: translations.totalAcademicHours,
      isOrderable: true
    },
  ];

  const rows = state.subjects.map(s => {
    const isSelected = selectedSubjects.includes(s.id);
    return <TableRow key={s.id}
      onClick={() => subjectSelectionChanged(s.id, !isSelected)}
      style={{ cursor: 'pointer' }}>
      <TableCell padding="checkbox">
        <Checkbox checked={isSelected} onChange={(e, isChecked) => subjectSelectionChanged(s.id, isChecked)} />
      </TableCell>
      <TableCell align="center">{s.code}</TableCell>
      <TableCell align="center">{s.name}</TableCell>
      <TableCell align="center">{s.semester}</TableCell>
      <TableCell align="center">{s.career}</TableCell>
      <TableCell align="center">{s.classroomType}</TableCell>
      <TableCell align="center">{s.academicHoursPerWeek}</TableCell>
      <TableCell align="center">{s.totalAcademicHours}</TableCell>
    </TableRow>;
  });

  return <Container>
    <PageTitle title={translations.subjects} showLoading={state.isBusy} />
    <CustomTableToolbar
      deleteDialogTitle={translations.deleteSubjects}
      selectedNumberOfItems={selectedSubjects.length}
      onDeleteClick={onDeleteClick}
      onEditClick={onEditClick} />
    <CustomTableSearch
      showSearch
      showItemsPerPage
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

export default Subjects
