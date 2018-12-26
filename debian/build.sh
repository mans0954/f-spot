cd /src/f-spot
# Danger, the following command will zap any uncommited changes to /debian
# git clean -x -f -d
UPSTREAM_VERSION=`dpkg-parsechangelog --show-field Version | sed 's/-[^-]*$//'`
rm ../f-spot_$UPSTREAM_VERSION.orig.tar.gz
git submodule foreach git clean -x -f -d
git submodule update
nuget restore
gbp buildpackage --git-debian-branch=ubuntu/bionic --git-upstream-tree=master --git-submodules --git-export-dir=../ --git-verbose -us -uc
