# Nexus SDK Repository

Welcome to the Nexus SDK repository. In here you will find all the tools required to make your connection with Nexus even easier!

## Repository Overview

This repository consists of the following projects:

1. [Nexus Token SDK](./Nexus.Token.SDK) - This project contains functionalities required to interact with your Nexus Token environments.
2. Examples
    - [Stellar](../Nexus.Token.Stellar.Examples) examples of this SDK in action on the Stellar blockchain.
    - [Algorand](../Nexus.Token.Algorand.Examples) examples of this SDK in action on the Algorand blockchain.

## Installation using Nuget

This section contains information on how to install these SDKs in your projects.

- Add a new file called `nuget.config` to the solution folder (same directory as your `.sln` file).

```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <packageSources>
    <add key="NexusSDKs" value="https://gitlab.com/api/v4/projects/37897064/packages/nuget/index.json" />
  </packageSources>
</configuration>
```

- The SDK packages are now available to be installed under `NexusSDKs`.