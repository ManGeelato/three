import {
  Box,
  Button,
  FormControl,
  Grid,
  InputLabel,
  MenuItem,
  OutlinedInput,
  Select,
} from '@material-ui/core';
import { Incident, ListParams } from 'models';
import React, { ChangeEvent, useRef } from 'react';

import { Search } from '@material-ui/icons';

interface IncidentFiltersProps {
  filter: ListParams;
  incidentList: Incident[];
  onChange?: (newFilter: ListParams) => void;
  onSearchChange?: (newFilter: ListParams) => void;
}

const IncidentFilters = ({
  filter,
  incidentList,
  onChange,
  onSearchChange,
}: IncidentFiltersProps) => {
  const searchRef = useRef<HTMLInputElement>();

  const handleSearchChange = (e: ChangeEvent<HTMLInputElement>) => {
    if (!onSearchChange) return;

    const newFilter: ListParams = {
      ...filter,
      name_like: e.target.value,
      _page: 1,
    };

    onSearchChange(newFilter);
  };

  const handleIncidentChange = (
    e: ChangeEvent<{ name?: string; value: unknown }>
  ) => {
    if (!onChange) return;

    const newFilter: ListParams = {
      ...filter,
      _page: 1,
      incident: e.target.value || undefined,
    };
    onChange(newFilter);
  };

  const handleSortChange = (
    e: ChangeEvent<{ name?: string; value: unknown }>
  ) => {
    if (!onChange) return;

    const value = e.target.value;
    const [_sort, _order] = (value as string).split('.');

    const newFilter: ListParams = {
      ...filter,
      _sort: _sort || undefined,
      _order: (_order as 'asc' | 'desc') || undefined,
    };
    onChange(newFilter);
  };

  const handleClearFilter = () => {
    if (!onChange) return;

    const newFilter: ListParams = {
      ...filter,
      _page: 1,
      incident: undefined,
      _sort: undefined,
      _order: undefined,
      name_like: undefined,
    };
    onChange(newFilter);

    if (searchRef.current) {
      searchRef.current.value = '';
    }
  };

  return (
    <Box>
      {/* <Grid container spacing={3}>
        <Grid item xs={6} md={3}>
          <FormControl fullWidth variant="outlined" size="small">
            <InputLabel htmlFor="searchByName">Search by name</InputLabel>
            <OutlinedInput
              id="searchByName"
              label="Search by name"
              endAdornment={<Search />}
              defaultValue={filter.name_like}
              onChange={handleSearchChange}
              inputRef={searchRef}
            />
          </FormControl>
        </Grid>

        <Grid item xs={6} md={3}>
          <FormControl variant="outlined" size="small" fullWidth>
            <InputLabel id="filterByIncident">Filter by incident</InputLabel>
            <Select
              labelId="filterByIncident"
              id="demo-simple-select-outlined"
              value={filter.incident || ''}
              onChange={handleIncidentChange}
              label="Filter by incident"
            >
              <MenuItem value="">
                <em>All</em>
              </MenuItem>

              {incidentList.map((incident) => (
                <MenuItem key={incident.id} value={incident.id}>
                  {incident.title}
                </MenuItem>
              ))}
            </Select>
          </FormControl>
        </Grid>

        <Grid item xs={6} md={3}>
          <FormControl variant="outlined" size="small" fullWidth>
            <InputLabel id="sortBy">Sort</InputLabel>
            <Select
              labelId="sortBy"
              id="demo-simple-select-outlined"
              value={filter._sort ? `${filter._sort}.${filter._order}` : ''}
              onChange={handleSortChange}
              label="Sort"
            >
              <MenuItem value="">
                <em>No sort</em>
              </MenuItem>

              <MenuItem value="name.asc">Name ASC</MenuItem>
              <MenuItem value="name.desc">Name DESC</MenuItem>
              <MenuItem value="mark.asc">Mark ASC</MenuItem>
              <MenuItem value="mark.desc">Mark DESC</MenuItem>
            </Select>
          </FormControl>
        </Grid>

        <Grid item xs={6} md={3}>
          <Button
            variant="contained"
            color="primary"
            fullWidth
            onClick={handleClearFilter}
          >
            Clear
          </Button>
        </Grid>
      </Grid> */}
    </Box>
  );
};

export default IncidentFilters;
