import { InMemoryDbService } from 'angular-in-memory-web-api';

import { UserProblemData } from './data'
import { UserProblemDto } from '../solution-to-problem/model/UserProblemDto'


export class AppData implements InMemoryDbService {

  createDb(): { userProblems: UserProblemDto[]} {
    const userProblems = UserProblemData.usersProblems;
    return { userProblems };
  }
}
