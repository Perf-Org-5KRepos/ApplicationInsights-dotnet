Fix Issue # .
<Short description of the fix.>

- [ ] I ran Unit Tests locally.
- [ ] CHANGELOG.md updated with one line description of the fix, and a link to the original issue if available.

For significant contributions please make sure you have completed the following items:

- [ ] Design discussion issue #
- [ ] Changes in public surface reviewed
- [ ] CHANGELOG.md updated with one line description of the fix, and a link to the original issue.

The PR will trigger build, unit tests, and functional tests automatically. Please follow [these](https://github.com/Microsoft/ApplicationInsights-dotnet/blob/develop/.github/CONTRIBUTING.md) instructions to build and test locally.

Notes for reviewers:

- We support [comment build triggers](https://docs.microsoft.com/azure/devops/pipelines/repos/github?view=azure-devops&tabs=yaml#comment-triggers)
  - `/AzurePipelines run` will queue all builds
  - `/AzurePipelines run <pipeline-name>` will queue a specific build