import { ChangeDetectionStrategy, Component } from '@angular/core';
import { catchError, EMPTY, Subject } from 'rxjs';
import { DeborathProblemService } from '../deborath-problem.service';


@Component({
  selector: 'app-deborath-product-form',
  templateUrl: './deborath-product-form.component.html',
  styleUrls: ['./deborath-product-form.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush // INFO !!!! only detect chnages made from input properties, and events from child components (output),
  // and observables bound in the teamplate using an async pipe
  // bound values set in local properties won't trigger chnage detection, so won't update the ui
})
export class DeborathProductFormComponent {
  title = 'Problem';
  //errorMessage = '';
  private errorMessageSubject = new Subject<string>()
  errorMessage$ = this.errorMessageSubject.asObservable();

  // INFO problem observable data item stream, which emits Problem or undefined
  problem$ = this.problemService.selectedProblem$
  .pipe(
    catchError(err => {
      //this.errorMessage = err; //when changeDetection: ChangeDetectionStrategy.OnPush then this won't trigger chnage detection and error will not appear to the user
      this.errorMessageSubject.next(err); // emit value to the stream
      return EMPTY;
    })
  );

  constructor(private problemService: DeborathProblemService) { }

}
