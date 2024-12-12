import * as yup from 'yup';
import { Box, Button, CircularProgress } from '@material-ui/core';
import {
  InputField,
} from 'components/FormFields';
import React, { useEffect, useState } from 'react';
import { Alert } from '@material-ui/lab';
import { Entity } from 'models';
import { useAppSelector } from 'app/hooks';
import { useForm } from 'react-hook-form';
import { yupResolver } from '@hookform/resolvers/yup';
import { useSelector } from 'react-redux';

interface EntityFormProps {
  initialValues?: Entity;
  onSubmit?: (formValues: Entity) => void;
}

const EntityForm = ({ initialValues, onSubmit }: EntityFormProps) => {
  const schema = yup.object().shape({
    name: yup
      .string()
      .required('Please enter name.')
      .test('two-words', 'Please enter at least two words', (value) =>
        !value ? true : value?.split(' ').filter((x) => !!x).length > 2
      ),
  });

  const [error, setError] = useState('');

  const {
    control,
    handleSubmit,
    formState: { isSubmitting },
  } = useForm<Entity>({
    defaultValues: initialValues,
    resolver: yupResolver(schema),
  });

  const handleFormSubmit = async (formValues: Entity) => {
    try {
      // Clear previous submission error
      setError('');

      await onSubmit?.(formValues);
    } catch (error) {
      if (error instanceof Error) { 
        setError(error.message);
      }
      
    }
  };
  return (
    <Box maxWidth={400}>
      <form onSubmit={handleSubmit(handleFormSubmit)}>
        {/* FORM FIELDS */}
        <InputField name="firstName" control={control} label="Full Name" />
        <InputField name="lastName" control={control} label="Last Name" />
        
        {error && <Alert severity="error">{error}</Alert>}

        <Box mt={3}>
          <Button
            type="submit"
            variant="contained"
            color="primary"
            disabled={isSubmitting}
          >
            {isSubmitting && <CircularProgress size={16} color="secondary" />}{' '}
            &nbsp;Save
          </Button>
        </Box>
      </form>
    </Box>
  );
};

export default EntityForm;
