import React, { useEffect, useState } from 'react'
import { TableContainer, Paper, Table, TableHead, TableRow, TableCell, TableBody, Container } from '@material-ui/core';
import { getAllTeachers } from '../../services/teacher.service';
import { IGetAllTeacherResponseDto } from '../../models';
import { useSnackbar } from 'notistack';
import { getErrorCodeTranslation } from '../../services/translations';
import translations from '../../services/translations';

interface State {
  teachers: IGetAllTeacherResponseDto[]
}

function Teachers() {
  const [state, setState] = useState<State>({
    teachers: []
  });
  const { enqueueSnackbar } = useSnackbar();

  useEffect(() => {
    getAllTeachers().then(resp => {

      if (!resp.succeed) {
        enqueueSnackbar(getErrorCodeTranslation(resp.errorMessageId), { variant: 'error', });
        return;
      }

      setState({ ...state, teachers: resp.result })
    });
  }, [])

  const rows = state.teachers.map(t => {
    const fullname = `${t.firstName} ${t.firstLastName}`;
    return <TableRow key={t.id}>
      <TableCell align="center">{t.identifierNumber}</TableCell>
      <TableCell align="center">{fullname}</TableCell>
      <TableCell align="center">{t.priority}</TableCell>
    </TableRow>;
  });

  return <Container>
    <TableContainer component={Paper}>
      <Table>
        <TableHead>
          <TableRow>
            <TableCell align="center">{translations.identifierNumber}</TableCell>
            <TableCell align="center">{translations.name}</TableCell>
            <TableCell align="center">{translations.priority}</TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {rows}
        </TableBody>
      </Table>
    </TableContainer>
  </Container>;
}

export default Teachers
