export interface UserProblem {
  problemId: string;
  userId: string;
  requiredSolutionTypes: string;
  description: string;
  category?: string
  isConfirmed: boolean;
  isRejected: boolean;
  createdAt: string;
  color: string;
  searchKey?: string[];
}
