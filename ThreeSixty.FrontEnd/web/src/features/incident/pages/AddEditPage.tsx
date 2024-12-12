import { Box, Typography, makeStyles } from '@material-ui/core';
import { Link, useHistory, useParams } from 'react-router-dom';
import { useEffect, useState } from 'react';
import { ChevronLeft } from '@material-ui/icons';
import { Incident } from 'models';
import IncidentForm from '../components/IncidentForm';
import incidentApi from 'api/incidentApi';
import { toast } from 'react-toastify';

const useStyles = makeStyles((theme) => ({
  back: {
    display: 'flex',
    alignItems: 'center',
  },
}));

const AddEditPage = () => {
  const classes = useStyles();

  const history = useHistory();

  const { incidentId } = useParams<{ incidentId: string }>();
  const isEdit = Boolean(incidentId);
  const [incident, setIncident] = useState<Incident>();

  // useEffect(() => {
  //   if (!incidentId) return;

  //   // IFFE
  //   (async () => {
  //     try {
  //       const data: Incident = await incidentApi.getIncidentById(parseInt(incidentId));
  //       setIncident(data);
  //     } catch (error) {
  //       console.log(`Failed to fetch incident details`, error);
  //     }
  //   })();
  // }, [incidentId]);

  // const handleIncidentFormSubmit = async (formValues: Incident) => {
  //   // Handle submit here, call API to add/update incident
  //   if (isEdit) {
  //     await incidentApi.updateIncident(formValues);
  //   } else {
  //     await incidentApi.addIncident(formValues);
  //   }

  //   // Show toast success
  //   const message = isEdit
  //     ? 'Edit incident successfully!'
  //     : 'Add incident successfully!';
  //   toast.success(message);

  //   // throw new Error('My testing error');

  //   // Redirect back to Incident list
  //   history.push('/admin/Incident/getAll');
  // };

  const initialValues: Incident = {
    incident: '',
    ...incident,
  } as Incident;

  return (
    <Box>
      <Link to="/admin/Incident/getAll">
        <Typography variant="caption" className={classes.back}>
          <ChevronLeft /> Back to incident list
        </Typography>
      </Link>

      <Typography variant="h5">
        {'Edit incident'}
      </Typography>

      {(!isEdit || Boolean(incident)) && (
        <Box mt={3}>
          <IncidentForm
            initialValues={initialValues}
            // onSubmit={handleIncidentFormSubmit}
          />
        </Box>
      )}
    </Box>
  );
};

export default AddEditPage;
