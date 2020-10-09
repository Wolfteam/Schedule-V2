import React, { Fragment, useState } from 'react';
import { faSearch } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import {
  Checkbox,
  Container,
  createStyles,
  darken,
  Fab,
  Grid,
  IconButton,
  InputAdornment,
  lighten,
  makeStyles,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  TableSortLabel,
  TextField,
  Theme,
  Toolbar,
  Tooltip,
  Typography,
} from '@material-ui/core';
import { Pagination } from '@material-ui/lab';
import PageTitle from '../../components/page-title/page-title';
import { IPaginatedResponseDto, ISubjectResponseDto } from '../../models';
import { Add, Delete, Edit } from '@material-ui/icons';
import translations from '../../services/translations';
import validator from 'validator';

const useStyles = makeStyles((theme: Theme) => createStyles({
  paginationContainer: {
    marginTop: '10px'
  },
  pagination: {
    display: 'flex',
    justifyContent: 'flex-end'
  },
  visuallyHidden: {
    border: 0,
    clip: 'rect(0 0 0 0)',
    height: 1,
    margin: -1,
    overflow: 'hidden',
    padding: 0,
    position: 'absolute',
    top: 20,
    width: 1,
  },
  fab: {
    zIndex: 100,
    position: 'fixed',
    bottom: 70,
    right: 20
  },
  selectedItemsText: {
    flex: '1 1 100%'
  },
  toolbar: {
    color:'white',
    backgroundColor: darken(theme.palette.primary.light, 0.05),
  }
}));

const response: IPaginatedResponseDto<ISubjectResponseDto> = {
  totalPages: 100,
  currentPage: 1,
  succeed: true,
  errorCode: '',
  errorMessage: '',
  records: 5,
  take: 5,
  totalRecords: 500,
  result: [
    {
      id: 1,
      code: 44103,
      subject: 'Antenas',
      career: 'Sistemas',
      subjectType: 'Teoria',
      semester: 'Electiva Cod-44',
      academicHoursPerWeek: 4,
      totalAcademicHours: 72
    },
    {
      id: 2,
      code: 46573,
      subject: 'Base de datos',
      career: 'Sistemas',
      subjectType: 'Laboratorio de computacion',
      semester: 'Electiva Cod-46',
      academicHoursPerWeek: 4,
      totalAcademicHours: 72
    },
    {
      id: 3,
      code: 41594,
      subject: 'Canalizaciones',
      career: 'Sistemas',
      subjectType: 'Teoria',
      semester: 'Electiva Cod-41',
      academicHoursPerWeek: 5,
      totalAcademicHours: 90
    },
    {
      id: 4,
      code: 44103,
      subject: 'Antenas',
      career: 'Sistemas',
      subjectType: 'Teoria',
      semester: 'Electiva Cod-44',
      academicHoursPerWeek: 4,
      totalAcademicHours: 72
    },
    {
      id: 5,
      code: 46573,
      subject: 'Base de datos',
      career: 'Sistemas',
      subjectType: 'Laboratorio de computacion',
      semester: 'Electiva Cod-46',
      academicHoursPerWeek: 4,
      totalAcademicHours: 72
    },
    {
      id: 6,
      code: 41594,
      subject: 'Canalizaciones',
      career: 'Sistemas',
      subjectType: 'Teoria',
      semester: 'Electiva Cod-41',
      academicHoursPerWeek: 5,
      totalAcademicHours: 90
    },
    {
      id: 7,
      code: 44103,
      subject: 'Antenas',
      career: 'Sistemas',
      subjectType: 'Teoria',
      semester: 'Electiva Cod-44',
      academicHoursPerWeek: 4,
      totalAcademicHours: 72
    },
    {
      id: 8,
      code: 46573,
      subject: 'Base de datos',
      career: 'Sistemas',
      subjectType: 'Laboratorio de computacion',
      semester: 'Electiva Cod-46',
      academicHoursPerWeek: 4,
      totalAcademicHours: 72
    },
    {
      id: 9,
      code: 41594,
      subject: 'Canalizaciones',
      career: 'Sistemas',
      subjectType: 'Teoria',
      semester: 'Electiva Cod-41',
      academicHoursPerWeek: 5,
      totalAcademicHours: 90
    },
  ]
};

interface State {
  currentPage: number;
  totalPages: number;
  itemsPerPage: number;
  orderBy: string;
  orderByAsc: boolean;
}

function Subjects() {
  const classes = useStyles();
  const [state, setState] = useState<State>({
    currentPage: 1,
    totalPages: 0,
    itemsPerPage: 5,
    orderBy: 'Code',
    orderByAsc: true
  });

  const sortDirectionChanged = (sortBy: string) => () => {
    setState({ ...state, orderByAsc: !state.orderByAsc, orderBy: sortBy });
  };
  const sortDirection = state.orderByAsc ? 'asc' : 'desc';
  const headerCells = ["", "Code", "Asignatura", "Semestre", "Carreras", "Tipo Materia", "Horas Academicas Semanales", "Horas Academicas Totales"];

  const header = headerCells.map(el => {
    if (validator.isEmpty(el)) {
      return <TableCell key="selected-items" align='center' padding='none' />
    }

    const visualSortIndicator = state.orderBy === el ? (
      <span className={classes.visuallyHidden}>
        {!state.orderByAsc ? 'sorted descending' : 'sorted ascending'}
      </span>
    ) : null;
    return <TableCell
      key={el}
      align='center'
      padding='none'
      sortDirection={state.orderBy === el ? sortDirection : false}>
      <TableSortLabel
        active={state.orderBy === el}
        direction={state.orderBy === el ? sortDirection : 'asc'}
        onClick={sortDirectionChanged(el)}>
        {el}
        {visualSortIndicator}
      </TableSortLabel>
    </TableCell>;
  });

  const rows = response.result.map(r => {
    return <TableRow key={r.id}>
      <TableCell padding="checkbox">
        <Checkbox checked={true} />
      </TableCell>
      <TableCell align="center">{r.code}</TableCell>
      <TableCell align="center">{r.subject}</TableCell>
      <TableCell align="center">{r.semester}</TableCell>
      <TableCell align="center">{r.career}</TableCell>
      <TableCell align="center">{r.subjectType}</TableCell>
      <TableCell align="center">{r.academicHoursPerWeek}</TableCell>
      <TableCell align="center">{r.totalAcademicHours}</TableCell>
    </TableRow>;
  });

  const searchIcon = <InputAdornment position="start" >
    <FontAwesomeIcon icon={faSearch} />
  </InputAdornment>;

  return <Container>
    <PageTitle title={translations.subjects} />
    <Toolbar className={classes.toolbar}>
      <Typography className={classes.selectedItemsText} color="inherit" variant="subtitle1" component="div">
        4 items selected
      </Typography>
      <Tooltip title="Edit">
        <IconButton aria-label="delete" >
          <Edit htmlColor='yellow' />
        </IconButton>
      </Tooltip>
      <Tooltip title="Delete">
        <IconButton aria-label="delete">
          <Delete htmlColor='red' />
        </IconButton>
      </Tooltip>
    </Toolbar>
    <Grid container justify="flex-end">
      <Grid item>
        <TextField variant="outlined"
          margin="normal"
          required
          size="small"
          label="Search"
          type="text"
          InputProps={{
            startAdornment: (searchIcon)
          }} />
      </Grid>
    </Grid>
    <Grid container justify="center" direction="column">
      <Grid item xs>
        <TableContainer>
          <Table>
            <TableHead>
              <TableRow>
                {header}
              </TableRow>
            </TableHead>
            <TableBody>
              {rows}
            </TableBody>
          </Table>
        </TableContainer>
      </Grid>
    </Grid>
    <Grid
      container
      justify="space-between"
      alignItems="center"
      spacing={2}
      className={classes.paginationContainer}>
      <Grid item xs>
        <p>Page {response.currentPage} of {response.totalPages}. Total results: {response.totalRecords}</p>
      </Grid>
      <Grid item xs>
        <Pagination className={classes.pagination} count={response.totalPages} color="primary" />
      </Grid>
    </Grid>
    <Fab color="primary" aria-label="add" className={classes.fab}>
      <Add />
    </Fab>
  </Container>;
}

export default Subjects
