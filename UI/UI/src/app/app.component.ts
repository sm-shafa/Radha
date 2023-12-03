import { Component } from '@angular/core';
import {BookService} from "./services/book.service";
import {IPenaltyBussinessQuery} from "./Model/penalty-bussiness-model";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  public checkin: any;
  public checkout: any;
  public country: any;
  public result: any;

  constructor(private bookService: BookService) {
  }


  public calculate(): void  {
    const model: IPenaltyBussinessQuery = {
      CountryId: this.country,
      DateCheckedIn: this.checkin,
      DateCheckedOut: this.checkout
    };

    this.bookService.getPenaltyBussiness(model).toPromise().then((result) => {
      console.log(result);
      this.result = result;
    });
  }
}
