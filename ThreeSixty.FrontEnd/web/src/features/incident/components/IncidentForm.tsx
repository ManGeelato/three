import * as yup from 'yup';
import { Box, Button, CircularProgress, FormControl, Grid, InputLabel, MenuItem, Select } from '@material-ui/core';
import { useEffect, useState } from 'react';
import { Alert } from '@material-ui/lab';
import { Incident } from 'models';
import incidentApi from 'api/incidentApi';
import { useHistory, useParams } from 'react-router-dom';
import axiosClient from '../../../api/axiosClient';

interface IncidentFormProps {
  initialValues?: Incident;
  onSubmit?: (formValues: Incident) => void;
}

const IncidentForm = ({ initialValues, onSubmit }: IncidentFormProps) => {
  const { incidentId } = useParams<{ incidentId: string }>();
  const isEdit = Boolean(incidentId);
  const history = useHistory();
  const [input, setInput] = useState({incidentStatusId: ''});
  const [error, setError] = useState('');
  const [incident, setIncident] = useState<Incident>();
  const [incidentStatus, setIncidentStatus] = useState<any[]>([]);

  const getIncidentStatus = async () => {
    const { data }  =  await axiosClient.get(`/IncidentStatus`);
    setIncidentStatus(data);
    console.log("incident status", incidentStatus);
  };

  useEffect(() => {
    getIncidentStatus();
  }, []);


  useEffect(() => {
    if (!incidentId) return;

    (async () => {
      try {
        const data: Incident = await incidentApi.getIncidentById(parseInt(incidentId));
        setIncident(data);
      } catch (error) {
        console.log(`Failed to fetch incident details`, error);
      }
    })();
  }, [incidentId]);

  var formValues: Incident;

  const handleIncidentFormSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    await incidentApi.updateIncident(formValues);
    history.push('/admin/Incident/getAll');
  };

  return (
    <Box maxWidth={400}>
      <form onSubmit={handleIncidentFormSubmit}>
        <Grid item xs={12} md={8}>
          <FormControl variant="outlined" size="small" fullWidth>
            <InputLabel id="filterByIncidentStatus">Incident Status</InputLabel> 
              <Select
                labelId="filterByIncidentStatus"
                id="demo-simple-select-outlined"
                label="Filter by incident"
                name="incidentStatusId"
                value={input.incidentStatusId}
                onChange={(e) => setInput({...input, [e.target.name as string]: e.target.value}) }
              >
                {incidentStatus.map((incident) => (
                  <MenuItem key={incident.id} value={incident.id}>
                  {incident.name}
                </MenuItem>
                ))}
            </Select>
          </FormControl>
        </Grid> 
        
        {error && <Alert severity="error">{error}</Alert>}

        <Box mt={3}>
          <Button
            type="submit"
            variant="contained"
            color="primary"
            // disabled={isSubmitting}
          >
            {/* {isSubmitting && <CircularProgress size={16} color="secondary" />}{' '} */}
            Save
          </Button>
        </Box>
      </form>
    </Box>
  );
};

export default IncidentForm;
