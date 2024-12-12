import {
  Button,
  Dialog,
  DialogActions,
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
  List,
  ListItem,
} from '@material-ui/core';
import { Entity, Incident, ListParams } from 'models';
import { useRef, useState } from 'react';
import { capitalizeString } from 'utils';
import { Search } from '@material-ui/icons';
import { selectEntityList } from '../entitySlice';
import { selectIncidentList } from 'features/incident/incidentSlice';
import { useAppSelector } from 'app/hooks';

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
    borderRadius: 25,
    margin: 15,
    textAlign: 'center',
  },
});

export interface EntityTableProps {
  entityList: Entity[];
  incidentMap: {
    [key: string]: Incident;
  };
  currentPage: number;
  pageSize: number;
  filter: ListParams;
  onEdit?: (entity: Entity) => void;
  onRemove?: (entity: Entity) => void;
  onChange?: (newFilter: ListParams) => void;
  onSearchChange?: (newFilter: ListParams) => void;
}

const EntityTable = ({
  entityList,
  currentPage,
  filter,
  pageSize,
  onChange,
}: EntityTableProps) => {
  const classes = useStyles();
  const cardClasses = cardStyles();

  const [open, setOpen] = useState(false);
  const [selectedEntity, setSelectedEntity] = useState<Entity>();
  const [searchQuery, setSearchQuery] = useState("");
  const [filterQuery, setFilterQuery] = useState("");
  const [sortQuery, setSortQuery] = useState("");
  const searchRef = useRef<HTMLInputElement>();

  // dispaying address on the dropdown
  let filteredEntities = entityList.filter((entity) => entity.address.includes(filterQuery));

  //sorting names ascending
  let sortedNames = entityList.filter((entity) => entity.firstName.toLowerCase().includes(sortQuery));
  const namesAscending = [...sortedNames].sort((a, b) => a.firstName > b.firstName ? 1 : -1);

  //sorting names descending
  const namesDescending = [...sortedNames].sort((a, b) => a.firstName > b.firstName ? -1 : 1);

  const handleClose = () => {
    setOpen(false);
  };

  const handleViewClick = (entity: Entity) => {
    // Set selected entity
    setSelectedEntity(entity);
    // Show confirm dialog
    setOpen(true);
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
    <>
      <TableContainer component={Paper}>
        <Grid container spacing={3}>
          <Grid item xs={6} md={3}>
            <FormControl fullWidth variant="outlined" size="small">
              <InputLabel htmlFor="searchByName">Search by first name</InputLabel>
              <OutlinedInput
                endAdornment={<Search />}
                onChange={e=> setSearchQuery(e.target.value)}
                inputRef={searchRef}
              ></OutlinedInput>
            </FormControl>
          </Grid>

          <Grid item xs={6} md={3}>
            <FormControl variant="outlined" size="small" fullWidth>
              <InputLabel id="filterByIncident">Filter by address</InputLabel> 
              <Select
                labelId="filterByIncident"
                id="demo-simple-select-outlined"
                label="Filter by incident"
              >
                <MenuItem value="">
                  <em>All</em>
                </MenuItem>

                {filteredEntities.map((entity) => (
                  <MenuItem key={entity.id} value={entity.id}>
                  {entity.address}
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
                label="Sort"
              >
                <MenuItem value="">
                  <em>No sort</em>
                </MenuItem>

                <MenuItem value="name.asc"> Name ASC
                  {namesAscending.map((entity) => (
                    <MenuItem key={entity.id} value={entity.id}>
                    {entity.firstName}
                  </MenuItem>
                  ))}
                </MenuItem>
               
               
                <MenuItem value="name.desc">Name DESC
                  {namesDescending.map((entity) => (
                    <MenuItem key={entity.id} value={entity.id}>
                    {entity.firstName}
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
              <TableCell>ID Number</TableCell>
              <TableCell>First Name</TableCell>
              <TableCell>Surname</TableCell>
              <TableCell>Created By</TableCell>
              <TableCell>Address</TableCell>
              <TableCell align="right">Actions</TableCell>
            </TableRow>
          </TableHead>

          <TableBody>
            {entityList.filter((entity) => entity.firstName.toLowerCase().includes(searchQuery)).map((entity, index)=>(
              <TableRow key={entity.id}>
                <TableCell>
                  {index + 1 + pageSize * (currentPage - 1)}
                </TableCell>
                <TableCell>{capitalizeString(entity.firstName)}</TableCell>
                <TableCell>{capitalizeString(entity.lastNane)}</TableCell>
                <TableCell>{capitalizeString(entity.createdBy)}</TableCell>
                <TableCell>{capitalizeString(entity.address)}</TableCell>
                <TableCell align="right">
                  <Button
                  size="small"
                  variant="contained"
                  color="primary"
                  onClick={() => handleViewClick(entity)}
                  >
                  View Entity
                  </Button>
                </TableCell>
              </TableRow>
            ))}
          </TableBody>
        </Table>
      </TableContainer>

      {/* Remove Entity Dialog */}
      <Dialog
        open={open}
        keepMounted
        onClose={handleClose}
        aria-labelledby="alert-dialog-title"
        aria-describedby="alert-dialog-description"
      >

      <Grid item xs={12} md={8} className={cardClasses.card}>
        <List>
          <ListItem>Full Names: {selectedEntity?.firstName} {selectedEntity?.lastNane}</ListItem>
          {/* <ListItem>Date of Birth: {selectedEntity?.dateOfBirth}</ListItem> */}
          <ListItem>Full Address: {selectedEntity?.address}</ListItem>
          <ListItem>Created By: {selectedEntity?.createdBy}</ListItem>
        </List>
      </Grid>
      {/* <Card className={cardClasses.card}>
        <CardContent>
          <Typography variant="h5"> Entity Details</Typography>
          <Typography>Full Names: {selectedEntity?.firstName} {selectedEntity?.lastNane}</Typography>
          <Typography>Date of Birth: {selectedEntity?.dateOfBirth} </Typography>
          <Typography>Full Address: {selectedEntity?.address} </Typography>
          <Typography>Created By: {selectedEntity?.createdBy} </Typography>
        </CardContent>
      </Card>  */}
        <DialogActions>
          <Button onClick={handleClose} color="primary" variant="outlined">
            Close
          </Button>
        </DialogActions>
      </Dialog>
    </>
  );
};

export default EntityTable;