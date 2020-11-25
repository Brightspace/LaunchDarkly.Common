#!/bin/bash
set -eu

git_ref=$GITHUB_REF
git_run=$GITHUB_RUN_NUMBER
git_sha=$GITHUB_SHA
version_prefix=$VERSION_PREFIX

if [ $git_ref -eq "refs/tags/v$version_prefix" ]; then
	version_suffix=""
elif [ $git_ref -eq "refs/heads/main" ]; then
	version_suffix="-rc.$git_run"
else
	version_suffix="-alpha.$git_run"
fi

version="$version_prefix$version_suffix"
informational_version="$version+$git_sha"

cat <<EOT >> VersionInfo.props
<Project>
	<PropertyGroup>
		<Version>$version</Version>
		<AssemblyVersion>$version</AssemblyVersion>
		<FileVersion>$version</FileVersion>
		<InformationalVersion>$informational_version</InformationalVersion>
		<RepositoryBranch>$git_ref</RepositoryBranch>
		<RepositoryCommit>$git_sha</RepositoryCommit>
	</PropertyGroup>
</Project>
EOT

cat VersionInfo.props
