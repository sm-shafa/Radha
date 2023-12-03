import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {map} from "rxjs/operators";
import {Observable} from "rxjs";
import {environment} from "../../environments/environment";
import {ICalculationBookQuery} from "../Model/penalty-bussiness-model";

@Injectable({
  providedIn: 'root'
})
export class BookService {

  apiUrl = environment.apiUrl;

  constructor(
    private http: HttpClient) {

  }

  public getCalculationBook(body: ICalculationBookQuery): Observable<any> {
    const headers = {'content-type': 'application/json'}
    const data = JSON.stringify(body);
    return this.http.post(`${this.apiUrl}Book/GetCalculationBook`, data, {'headers':headers}).pipe(map((result) => {
        return result;
      })
    );
  }
}
