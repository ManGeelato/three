import { Entity, ListResponse } from './../models';

import { ListParams } from './../models/common';
import axiosClient from './axiosClient';

const entityApi = {
  getAll(params: ListParams): Promise<ListResponse<Entity>> {
    const url = '/entities/getAll';
    console.log("params", params);
    return axiosClient.get(url, {
      params,
    });
  },

  getDashboard(params: ListParams): Promise<ListResponse<Entity>> {
    const url = '/Dashboard/getAll';
    console.log("params", params);
    return axiosClient.get(url, {
      params,
    });
  },
  
  getById(id: number): Promise<Entity> {
    const url = `/entities/${id}`;
    return axiosClient.get(url);
  },

  add(data: Entity): Promise<Entity> {
    const url = '/entities/add';
    return axiosClient.post(url, data);
  },

  update(data: Partial<Entity>): Promise<Entity> {
    const url = `/entities/${data.id}`;
    return axiosClient.patch(url, data);
  },

  remove(id: number): Promise<any> {
    const url = `/entities/${id}`;
    return axiosClient.delete(url);
  },
};

export default entityApi;
