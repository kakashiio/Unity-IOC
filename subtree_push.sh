rm -rf Unity-IOC/Assets/IO.Unity3D.Source/IOC/Samples
rm -rf Unity-IOC/Assets/IO.Unity3D.Source/IOC/Samples.meta
git subtree push --prefix Unity-IOC/Assets/IO.Unity3D.Source/IOC https://github.com/kakashiio/Unity-IOC.git 1.0.0
ln -s Unity-IOC/Assets/IO.Unity3D.Source/IOC/Samples~ Unity-IOC/Assets/IO.Unity3D.Source/IOC/Samples
