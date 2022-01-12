Note: the Swift solutions are built in Xcode Playgrounds. Since there is no way to add SPM package references to a playground without also having a project in your workspace, I added SPM packages as git submodules to this repository so that they can be added to the Xcode workspace as local packages.

This means that when cloning this repo, you have to initialize the submodules to pull in the Swift packages. You can do this by running this from the repository root:

    git submodule update --init