import { UserProblem } from "../solution-to-problem/model/UserProblem";
import { ProblemCategory } from "../solution-to-problem/model/ProblemCategory";

export class UserProblemData {
  static usersProblems: UserProblem[] = [
    {
      problemId: "f6a4f74e-4b0a-4487-a6ff-ca2244b4afd8",
      userId: "c1bfe7bc-053c-465b-886c-6f55af7ec4fe",
      requiredSolutionTypes: "PocInCode; PlanOfImplmentingChangeInCode",
      description: "refactor to cqrs instead of not cohesive services, srp against separate user use case",
      categoryId: "00000000-0000-0000-0000-000000000006",
      isConfirmed: false,
      isRejected: false,
      createdAt: "",
      color: "	#000000"
    },
    {
      problemId: "922a401f-908d-4c45-be4c-e6b8aad6cbcb",
      userId: "c1bfe7bc-053c-465b-886c-6f55af7ec4fe",
      requiredSolutionTypes: "TheoryOfConceptWithExample",
      description: "refactor anemic entity + service into ddd object oriented model (entities with behaviors, aggreagtes, value objects)",
      categoryId: "00000000-0000-0000-0000-000000000006",
      isConfirmed: false,
      isRejected: true,
      createdAt: "",
      color: "	#000000"
    },
    {
      problemId: "46b71ff1-747c-43b2-ab7a-1937e6c43913",
      userId: "c1bfe7bc-053c-465b-886c-6f55af7ec4fe",
      requiredSolutionTypes: "RoiAnalysis",
      description: "introduce integration tests and unit test into existing code",
      categoryId: "00000000-0000-0000-0000-000000000010",
      isConfirmed: true,
      isRejected: false,
      createdAt: "",
      color: "	#000000"
    },
  ]

  static problemCategories : ProblemCategory[] = [
    {
      id: "00000000-0000-0000-0000-000000000006",
      name : "architecture"
    },
    {
      id: "00000000-0000-0000-0000-000000000010",
      name : "tests"
    }
  ]
}
