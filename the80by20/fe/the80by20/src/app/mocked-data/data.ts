import { UserProblemDto } from "../solution-to-problem/model/UserProblemDto";

export class UserProblemData {
  static usersProblems: UserProblemDto[] = [
    {
      problemId: "f6a4f74e-4b0a-4487-a6ff-ca2244b4afd8",
      userId: "c1bfe7bc-053c-465b-886c-6f55af7ec4fe",
      requiredSolutionTypes: "PocInCode; PlanOfImplmentingChangeInCode",
      description: "refactor to cqrs instead of not cohesive services",
      category: "",
      isConfirmed: false,
      isRejected: false,
      workingOnSolutionEnded: false,
      createdAt: ""
    },
    {
      problemId: "922a401f-908d-4c45-be4c-e6b8aad6cbcb",
      userId: "c1bfe7bc-053c-465b-886c-6f55af7ec4fe",
      requiredSolutionTypes: "TheoryOfConceptWithExample",
      description: "refactor anemic entity + service into ddd object oriented model (entities with behaviors, aggreagtes)",
      category: "",
      isConfirmed: false,
      isRejected: false,
      workingOnSolutionEnded: false,
      createdAt: ""
    },
    {
      problemId: "46b71ff1-747c-43b2-ab7a-1937e6c43913",
      userId: "c1bfe7bc-053c-465b-886c-6f55af7ec4fe",
      requiredSolutionTypes: "RoiAnalysis",
      description: "show how to create integration tests and unit test in existing code",
      category: "",
      isConfirmed: false,
      isRejected: false,
      workingOnSolutionEnded: false,
      createdAt: ""
    },
  ]
}
