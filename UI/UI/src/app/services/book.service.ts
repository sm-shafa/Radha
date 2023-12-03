import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {IPenaltyBussinessQuery} from "../models/penalty-bussiness-model";
import {map} from "rxjs/operators";
import {Observable} from "rxjs";
import {environment} from "../../environments/environment";

@Injectable({
  providedIn: 'root'
})
export class BookService {

  apiUrl = environment.apiUrl;

  constructor(
    private http: HttpClient) {

  }

  public getPenaltyBussiness(body: IPenaltyBussinessQuery): Observable<any> {
    const headers = {'content-type': 'application/json'}
    const data = JSON.stringify(body);
    return this.http.post(`${this.apiUrl}Book/GetPenaltyBussinessDay`, data, {'headers':headers}).pipe(map((result) => {
        return result;
      })
    );
  }
}
