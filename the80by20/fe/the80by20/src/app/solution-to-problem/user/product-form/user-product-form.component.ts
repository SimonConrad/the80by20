import { ChangeDetectionStrategy, Component } from '@angular/core';
import { catchError, EMPTY, Observable, Subject, tap } from 'rxjs';
import { UserProblem } from '../model/UserProblem';
import { UserProblemService } from '../user-problem.service';
import { UUID } from 'angular2-uuid';



@Component({
  selector: 'app-user-product-form',
  templateUrl: './user-product-form.component.html',
  styleUrls: ['./user-product-form.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush // INFO !!!! only detect chnages made from input properties, and events from child components (output),
  // and observables bound in the teamplate using an async pipe
  // bound values set in local properties won't trigger chnage detection, so won't update the ui
})
export class UserProductFormComponent {
  title = 'User Problem From';

  private errorMessageSubject = new Subject<string>()
  errorMessage$ = this.errorMessageSubject.asObservable();

  selectedProblem$: Observable<UserProblem | undefined>;

  updateProblem$: Observable<UserProblem>;
  addProblem$: Observable<UserProblem>;

  constructor(private problemService: UserProblemService) {
    // INFO problem observable data item stream, which emits Problem or undefined
    this.selectedProblem$ = this.problemService.selectProblemActionStream$
      .pipe(
        catchError(err => {
          //this.errorMessage = err; //when changeDetection: ChangeDetectionStrategy.OnPush then this won't trigger chnage detection and error will not appear to the user
          this.errorMessageSubject.next(err); // emit value to the stream
          return EMPTY;
        })
      );

    this.addProblem$ = this.problemService.addProblemActionStream$.pipe(
      catchError(err => {
        this.errorMessageSubject.next(err); // emit value to the stream
        return EMPTY;
      }),
      tap(() => this.problemService.startSelectAction(undefined))
      );

    this.updateProblem$ = this.problemService.updateProblemActionStream$.pipe(
      catchError(err => {
        this.errorMessageSubject.next(err); // emit value to the stream
        return EMPTY;
      }),
      tap(() => this.problemService.startSelectAction(undefined)),
      );
  }

  //startEdit
  onSave(problem: UserProblem): void {

    //INFO alternative cal update service and subscribe to invoke it, rember then on need to unsubcribing in ondestroy

    if(problem.id === ''){
      problem.id = UUID.UUID();
      this.problemService.startAdd(problem);
    } else{
      this.problemService.startUpdate(problem);
    }
      // let newProblem: UserProblem =
    // {
    //   id: userProblem.id,
    //   userId: "c1bfe7bc-053c-465b-886c-6f55af7ec4fe",
    //   requiredSolutionTypes: "PocInCode; PlanOfImplmentingChangeInCode",
    //   description: "edit",
    //   categoryId: "00000000-0000-0000-0000-000000000006",
    //   category: "architecture",
    //   isConfirmed: false,
    //   isRejected: false,
    //   createdAt: "",
    //   color: "	#000000"
    // };
  }

  onCancel(): void {
    this.problemService.startSelectAction(undefined);
  }

}
