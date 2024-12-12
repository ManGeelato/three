import {
  Button,
  Paper,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  makeStyles,
  FormControl,
  Grid,
  InputLabel,
  OutlinedInput,
  Select,
  MenuItem,
} from '@material-ui/core';
import {  Incident, ListParams } from 'models';
import { useRef, useEffect, useState } from 'react';
import { Search } from '@material-ui/icons';


const useStyles = makeStyles((theme) => ({
  table: {},
  edit: {
    marginRight: theme.spacing(1),
  },
}));

const cardStyles = makeStyles({
  card: {
    width: 500,
    height:250,
    backgroundColor: "#3F51B5",
    color:"white",
    borderRadius: 25,
    margin: 15,
    textAlign: 'center',
  },
});

export interface IncidentTableProps {
  incidentList: Incident[];
  incidentMap: {
    [key: string]: Incident;
  };
  currentPage: number;
  pageSize: number;
  filter: ListParams;
  onEdit?: (incident: Incident) => void;
  onRemove?: (incident: Incident) => void;
  onChange?: (newFilter: ListParams) => void;
  onSearchChange?: (newFilter: ListParams) => void;
}

const IncidentTable = ({
  incidentList,
  currentPage,
  filter,
  pageSize,
  onChange,
  onEdit,
}: IncidentTableProps) => {
  const classes = useStyles();
  const cardClasses = cardStyles();

  const [open, setOpen] = useState(false);
  const [searchQuery, setSearchQuery] = useState("");
  const [filterQuery, setFilterQuery] = useState("");
  const [sortQuery, setSortQuery] = useState("");
  const searchRef = useRef<HTMLInputElement>();


  // dispaying address on the dropdown
  let filteredEntities = incidentList.filter((incident) => incident.incidentTypeName.includes(filterQuery));

  //sorting names ascending
  let sortedNames = incidentList.filter((incident) => incident.incidentTypeName.toLowerCase().includes(sortQuery));
  const namesAscending = [...sortedNames].sort((a, b) => a.incidentTypeName > b.incidentTypeName ? 1 : -1);

  //sorting names descending
  const namesDescending = [...sortedNames].sort((a, b) => a.incidentTypeName > b.incidentTypeName ? -1 : 1);

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
    <>
      <TableContainer component={Paper}>
        <Grid container spacing={3}>
          <Grid item xs={6} md={3}>
          <FormControl fullWidth variant="outlined" size="small">
              <InputLabel htmlFor="searchByName">Search by Incident</InputLabel>
              <OutlinedInput
                endAdornment={<Search />}
                onChange={e=> setSearchQuery(e.target.value)}
                inputRef={searchRef}
              ></OutlinedInput>
            </FormControl>
          </Grid>

          <Grid item xs={6} md={3}>
          <FormControl variant="outlined" size="small" fullWidth>
              <InputLabel id="filterByIncident">Filter by Incident Type</InputLabel> 
              <Select
                labelId="filterByIncident"
                id="demo-simple-select-outlined"
                label="Filter by incident"
              >
                <MenuItem value="">
                  <em>All</em>
                </MenuItem>

                {filteredEntities.map((incident) => (
                  <MenuItem key={incident.id} value={incident.id}>
                  {incident.incidentTypeName}
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
                // value={sortQuery}
                // onChange={handleSortChange}
                label="Sort"
              >
                <MenuItem value="">
                  <em>No sort</em>
                </MenuItem>

                <MenuItem value="name.asc"> Name ASC
                  {namesAscending.map((incident) => (
                    <MenuItem key={incident.id} value={incident.id}>
                    {incident.incidentTypeName}
                  </MenuItem>
                  ))}
                </MenuItem>
               
                <MenuItem value="name.desc">Name DESC
                  {namesDescending.map((incident) => (
                    <MenuItem key={incident.id} value={incident.id}>
                    {incident.incidentTypeName}
                  </MenuItem>
                  ))}
                </MenuItem>
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
        </Grid>
        <Table className={classes.table} size="small" aria-label="simple table">
          <TableHead>
            <TableRow>
              <TableCell>ID.</TableCell>
              <TableCell>Incident Type</TableCell>
              <TableCell>Incident Status</TableCell>
              <TableCell>Title</TableCell>
              <TableCell>Description</TableCell>
              <TableCell>Entity Name</TableCell>
              <TableCell>Entity Address</TableCell>
              <TableCell>Incident Date</TableCell>
              <TableCell align="right">Actions</TableCell>
            </TableRow>
          </TableHead>

          <TableBody>
            {incidentList.filter((incident) => incident.incidentTypeName.toLowerCase().includes(searchQuery)).map((incident, index) => (
              <TableRow key={incident.id}>
                <TableCell>
                  {index + 1 + pageSize * (currentPage - 1)}
                </TableCell>
                <TableCell>{incident.incidentTypeName}</TableCell>
                <TableCell>{incident.incidentStatusName}</TableCell>
                <TableCell>{incident.shortDescription}</TableCell>
                <TableCell>{incident.longDescription}</TableCell>
                <TableCell>{incident.firstName} {incident.lastNane}</TableCell>
                <TableCell>{incident.address}</TableCell>
                <TableCell>{incident.incidentDate}</TableCell>
               
                <TableCell align="right">
                  <Button
                    size="small"
                    variant="contained"
                    className={classes.edit}
                    color="primary"
                    onClick={() => onEdit?.(incident)}
                  >
                    Edit
                  </Button>
                </TableCell>
              </TableRow>
            ))}
          </TableBody>
        </Table>
      </TableContainer>
    </>
  );
};

export default IncidentTable;