export interface Entity {
  id?: number;
  
  idnumber: string;
  firstName: string;
  lastNane: string;
  dateOfBirth: string;
  address: string;
  homePhoneNumber: string;
  workPhoneNumber: string;
  mobilePhoneNumber: string;
  emailAddress: string;
  createdBy: string;
  numberOfIncidentThisMonth: number;
  numberOfIncidentThisWeek: number;
  numberOfIncidentToday:number;
  totalNumberOfIncidentsAllTime: number;

}