import { Incident, ListResponse, ListParams } from "models";
import axiosClient from "./axiosClient";

const incidentApi = {
    getAll(params: ListParams): Promise<ListResponse<Incident>>{
        const incidentsUrl = '/Incident/getAll';
        return axiosClient.get(incidentsUrl, {
            params
        });
    },

    getIncidentById(id: number) : Promise<Incident>{
        const singleIncidentUrl = `/Incident/${id}`;
        return axiosClient.get(singleIncidentUrl);
    },

    addIncident(incidentObject: Incident) : Promise<Incident>{
        const incidentToAddUrl = '/Incident/add';
        return axiosClient.post(incidentToAddUrl, incidentObject);
    },

    updateIncident(incidentObject: Partial<Incident>) : Promise<Incident>{
        const incidentToUpdateUrl = `/Incident/${incidentObject.id}`;
        return axiosClient.put(incidentToUpdateUrl, incidentObject);
    },

    searchIncident(incidentDate: Date) : Promise<Incident>{
        const incidentToSearchUrl = `/Incident/Search/${incidentDate.toString()}`; //confirm the toString() method
        return axiosClient.get(incidentToSearchUrl);
    },

    // the below method won't be used
    deleteIncident(id: number) : Promise<Incident> {
        const incidentToDeleteUrl = `/Incident/${id}`;
        return axiosClient.delete(incidentToDeleteUrl);
    }
}

export default incidentApi;