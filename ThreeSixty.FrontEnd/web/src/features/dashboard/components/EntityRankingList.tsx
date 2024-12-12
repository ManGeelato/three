import {
  makeStyles,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
} from '@material-ui/core';
import { Entity } from 'models';


const useStyles = makeStyles({
  table: {},
});

interface EntityRankingListProps {
  entityList: Entity[];
}

const EntityRankingList = ({ entityList }: EntityRankingListProps) => {
  const classes = useStyles();

  return (
    <TableContainer>
      <Table className={classes.table} size="small" aria-label="simple table">
        <TableHead>
          <TableRow>
            <TableCell align="center">#</TableCell>
            <TableCell align="left">First Name</TableCell>
            <TableCell align="left">Last Name</TableCell>
          </TableRow>
        </TableHead>

        <TableBody>
          {entityList.map((entity, idx) => (
            <TableRow key={entity.id}>
              <TableCell align="center">{idx + 1}</TableCell>
              <TableCell align="left">{entity.firstName}</TableCell>
              <TableCell align="left">{entity.lastNane}</TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>
    </TableContainer>
  );
};

export default EntityRankingList;
