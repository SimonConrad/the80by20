export interface UserProblemDto {
  problemId: string;
  userId: string;
  requiredSolutionTypes: string;
  description?: string;
  category?: string
  isConfirmed: boolean;
  isRejected: boolean;
  workingOnSolutionEnded: boolean;
  createdAt: string;
}
