import { Review } from '../models/review';

export interface Customer {
  id?: number;
  firstname: string;
  lastname: string;
  birthday?: string;
  tele: string;
  address: string;
  avatar?: string|any;
  anamneseDate: string;
  anamnesePayed: boolean;
  diagnostikDate: string;
  diagnostikPayed: boolean;
  elternDate: string;
  elternPayed: boolean;
  problemHierarchy: string;
  reviews?: Review[];
}
