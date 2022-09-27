export interface UserProblem {
  id: string;
  userId: string;
  requiredSolutionTypes: string;
  description: string;
  categoryId?: string;
  category?: string
  isConfirmed: boolean;
  isRejected: boolean;
  createdAt: string;
  color: string;
  searchKey?: string[];
}
