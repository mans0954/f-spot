# Run from the grandparent folder with
# docker build f-spot/debian/ --tag f-spot
# docker run -ti -v `pwd`:/src f-spot

FROM ubuntu:bionic
RUN apt update
RUN apt install -y apt-transport-https dirmngr ca-certificates
RUN apt-key adv --keyserver hkp://keyserver.ubuntu.com:80 --recv-keys 3FA7E0328081BFF6A14DA29AA6A19B38D3D831EF
RUN echo "deb https://download.mono-project.com/repo/ubuntu stable-bionic main" | tee /etc/apt/sources.list.d/mono-official-stable.list
RUN apt update
RUN apt install -y mono-devel
RUN apt install pkg-config
RUN apt install  -y cli-common-dev libgtk2.0-dev libgtk2.0-cil-dev gtk-sharp2-gapi libgnome2-dev libcairo2-dev libmono-addins-cil-dev libmono-addins-gui-cil-dev libdbus2.0-cil-dev libdbus-glib2.0-cil-dev libgnome2.0-cil-dev libgconf2.0-cil-dev libgnome-vfs2.0-cil-dev  libgnome-keyring1.0-cil-dev liblcms2-dev libgnomeui-dev libexif-dev libjpeg-dev libunique-dev intltool gnome-doc-utils gnome-icon-theme libgtk-sharp-beans2.0-cil-dev libgio2.0-cil-dev libmono-posix4.0-cil nunit
RUN DEBIAN_FRONTEND=noninteractive apt install -y git-buildpackage nuget
RUN apt install quilt

CMD /src/f-spot/debian/build.sh
