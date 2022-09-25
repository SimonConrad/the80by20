import { Component, OnInit } from '@angular/core';

import { SolutionToProblemService } from './solution-to-problem.service';
import { UserProblemDto } from './model/UserProblemDto'
import { Observable } from 'rxjs';
@Component({
  selector: 'app-solution-to-problem',
  templateUrl: './solution-to-problem.component.html',
  styleUrls: ['./solution-to-problem.component.scss']
})
export class SolutionToProblemComponent implements OnInit {

  userProblems$ : Observable<UserProblemDto[]> | undefined;

  constructor(private service: SolutionToProblemService) { }

  ngOnInit(): void {
    this.userProblems$ = this.service.getUserProblems();
  }

}
