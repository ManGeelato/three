export interface Incident {
  id: number;
  
  incidentTypeId: number;
  incidentTypeName: string;
  incidentStatusName: string;
  incidentStatusId: number;
  title: string;
  entityId: number;
  firstName: string;
  lastNane: string;
  address: string;
  shortDescription:string;
  longDescription: string;
  incidentDate: string;
  pageCount: number;
  fileName: string;
  createdBy: string
}