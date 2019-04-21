import { Customer } from './../../customer/customer';

export class UebersichtModel {
  id?: number;
  customer?: Customer;
  reviews: ReviewEntries[];
}

class ReviewEntries {
  id?: number;
  date: Date;
  exercises: string;
  reasons: string;
  payed: boolean;
}
