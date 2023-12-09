import { Component } from '@angular/core';
import {BookService} from "./services/book.service";
import {ICalculationBookQuery} from "./Model/penalty-bussiness-model";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  public checkOut: Date;
  public returnDate: Date;
  public countryId: any;
  public bookCalculation: any;
  public error: string;

  constructor(private bookService: BookService) {
  }


  public calculate(): void  {
    this.error = '';
    if (!this.countryId || !this.returnDate || !this.checkOut) {
      this.error = 'Fill inputs';
      return;
    }

    const model: ICalculationBookQuery = {
      countryId: this.countryId,
      checkedOutDate: this.checkOut,
      returnDate: this.returnDate
    };

    this.bookService.getCalculationBook(model).toPromise().then((result) => {
      this.bookCalculation = result;
    }).catch((e)=> {
      console.log(e.error);
      this.error = e.error;
    });
  }
}
