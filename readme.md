# Information

This is part of a bachelor thesis done by the authors in 2023.

A link to the other parts of the project can be found below:

- [VR-Demo](https://github.com/Bacheloroppgave-Kystverket/Unity-VR-Eye-Tracking-Demo)
- [Frontend](https://github.com/Bacheloroppgave-Kystverket/Frontend)
- [Backend](https://github.com/Bacheloroppgave-Kystverket/Backend)

#Setup of demo
In order for the eye tracking demo to be ready both the backend and frontend has to be setup for the full experience.

Firstly the demo is run inside a unity editor. The editor used for this project is 2021.3.14f1. 

When the unity edtior is downloaded the project can be launched in unity. Launch the project and go to the [Oculus Integration SDK](https://developer.oculus.com/downloads/package/unity-integration/47.0)
and download the package versioned 47.0. In order to import the package follow the instructions under unity setup [here](https://developer.oculus.com/documentation/unity/move-overview/).

Clear all the errors after the package has been installed. If there is any errors with the scripts in Assets/EyeTracking project folder, then remove the imports form oculus in those files.

When the oculus package is imported the project is ready to be launched. Go to Assets/ Make sure that the Oculus app for the Quest Pro is installed.
And the eyetracking is enabled in the "Beta" section of the settings.

The main scene can be found in Assets/Eyetracking project/Scenes/controller functionality testing

When all this is done the project should be able to be launched without issue. The diffrent IP addresses can be changed in the AuthenticationManager. 
And login details also needs to be changed there.

If the hands of the user are purple the materials renderer only needs to be changed to "Universal render pipeline/Lit"
 
