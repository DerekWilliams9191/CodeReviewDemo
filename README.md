# Code Review Training Exercise
edit
Welcome to the Code Review Training Exercise! This hands-on exercise will help you learn the code review process by experiencing it firsthand through a guided GitHub workflow.

## Learning Objectives

By completing this exercise, you will learn:

1. How to identify and fix common code issues
2. The difference between functional correctness and code quality
3. How to respond to code review feedback
4. Best practices for C# development

## Instructions

### Getting Started

> Below we are just getting things set up. We have provided prewritten code that is supposed to mimic code you just wrote.

1. Fork this repository to your own GitHub account
2. Click **Branches** and then **New Branch**
3. Name your new branch something unique BUT MEMORABLE like your name, initials, etc.
4. Once created, click on your new branch name
5. Review the code in `src/CodeReviewTraining/OrderProcessor.cs`
6. Remove the commend at the top of the file. ONLY this comment, nothing else yet.
7. Click **Commit changes...** once the comment has been removed.

### Now we are ready to start

> In this scenario, you just wrote `OrderProcessor.cs` and your team is about to perform a code review on it.

### Step 1: Submit Your Initial PR

> You think your code is ready so you create a PR (Pull Request)
> It is called "Pull Request" because it is from the perspective of the software maintainer, and you are asking them to pull your code into their code.

1. Without making any changes yet, create a pull request from your branch to the main repository
   - Click **Pull requests** from the top
   - There should be a yellow banner with the option to **Compare & pull request**, click it!
2. This screen shows you the diff of all the code in this PR.
3. Click the green button **Create pull request**
3. The GitHub Action will run and leave feedback about the failing tests
4. Review the test failure details

### Step 2: Make the Tests Pass

> After 

1. Look for the code sections marked with `STEP 1` comments in `OrderProcessor.cs`
2. Uncomment the necessary code sections to make the tests pass:
   - Null checks
   - Input validation
   - Exception handling
3. Commit and push your changes to update the PR
4. Wait for the GitHub Action to run again

### Step 3: Address Code Quality Issues

1. Once the tests are passing, the GitHub Action will perform a code review
2. It will identify code quality issues that still need to be fixed
3. Look for the code sections marked with `STEP 2` comments in `OrderProcessor.cs`
4. Uncomment the necessary code sections to address each code quality issue:
   - Documentation
   - Logging
5. Commit and push your changes to update the PR again

### Step 4: Get PR Approval

1. Once all code quality issues are fixed, the GitHub Action will automatically approve your PR
2. Review the final feedback and reflect on what you've learned

## Code Review Checklist

During this exercise, you'll address these common code review checklist items:

- [ ] **Null checks**: Are all inputs validated for null?
- [ ] **Input validation**: Is the data validated before processing?
- [ ] **Exception handling**: Are exceptions properly caught and handled?
- [ ] **Documentation**: Is the code well-documented with XML comments?
- [ ] **Logging**: Are important operations and errors properly logged?

## Troubleshooting

- If your PR doesn't trigger the GitHub Action, check if you've created it correctly
- Make sure you're uncommenting the correct sections of code
- If tests are still failing, carefully review the test output for clues

## Next Steps

After completing this exercise, consider:

1. Creating your own code review checklist for future projects
2. Reviewing the unit tests to understand how they verify functionality
3. Adding additional improvements to the code beyond what was required
4. Setting up a similar workflow for your own projects

Good luck and happy coding!
