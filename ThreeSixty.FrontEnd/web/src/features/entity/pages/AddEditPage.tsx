import { Box, Typography, makeStyles } from '@material-ui/core';
import { Link, useHistory, useParams } from 'react-router-dom';
import React, { useEffect, useState } from 'react';

import { ChevronLeft } from '@material-ui/icons';
import { Entity } from 'models';
import EntityForm from '../components/EntityForm';
import entityApi from 'api/entityApi';
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

  const { entityId } = useParams<{ entityId: string }>();
  const isEdit = Boolean(entityId);
  const [entity, setEntity] = useState<Entity>();

  useEffect(() => {
    if (!entityId) return;

    // IFFE
    (async () => {
      try {
        const data: Entity = await entityApi.getById(parseInt(entityId));
        setEntity(data);
      } catch (error) {
        console.log(`Failed to fetch entity details`, error);
      }
    })();
  }, [entityId]);

  const handleEntityFormSubmit = async (formValues: Entity) => {
    // Handle submit here, call API to add/update entity
    if (isEdit) {
      await entityApi.update(formValues);
    } else {
      await entityApi.add(formValues);
    }

    // Show toast success
    const message = isEdit
      ? 'Edit entity successfully!'
      : 'Add entity successfully!';
    toast.success(message);

    // throw new Error('My testing error');

    // Redirect back to Entity list
    history.push('/admin/entities/getAll');
  };

  const initialValues: Entity = {
    incident: '',
    ...entity,
  } as Entity;

  return (
    <Box>
      <Link to="/admin/entities/getAll">
        <Typography variant="caption" className={classes.back}>
          <ChevronLeft /> Back to entity list
        </Typography>
      </Link>

      <Typography variant="h5">
        {isEdit ? 'Update entity info' : 'Add new entity'}
      </Typography>

      {(!isEdit || Boolean(entity)) && (
        <Box mt={3}>
          <EntityForm
            initialValues={initialValues}
            onSubmit={handleEntityFormSubmit}
          />
        </Box>
      )}
    </Box>
  );
};

export default AddEditPage;
