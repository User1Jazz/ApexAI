# Apex AI Documentation

## Content
- [Getting started](#getting-started)
    - [Installation](#installation)
    - [System Overview](#system-overview)
    - [Overview of Sample States](#overview-of-sample-states)
    - [Workflow](#workflow)
- [Defining Custom States](#defining-custom-states)

---

## Getting Started

### Installation
1. Download ApexAI [here](https://github.com/User1Jazz/ApexAI/releases)
2. In Unity Editor, navigate to `Assets->Import Package->Custom Package`
3. Select and import the downloaded file
    - when prompted, select all files from the package
4. Open the sample scene (`ApexAI/Samples/DemoScenes/Demo 0/Demo_0.unity`) to see ApexAI in action.
- **NOTE:** before you use ApexAI, make sure you [install dependencies](#dependencies)

### System Overview

#### Dependencies
- You can install required packages using Unity's package manager (`Window->Package Manager`)

ApexAI requires the following Unity packages in order to function:
```
AI Navigation (com.unity.ai.navigation) by Unity Technologies Inc.
```

#### System Diagram
![](/Assets/ApexAISystemDiagram.drawio.png)

#### AIStateMachine Class
- A master class in a state-machine model
- Tasked with initialising and updating agent's state at runtime

#### AIState Class
- Parent class for every state
- Defines functions to initialise, continuously run, and exit the state, as well as the function to check transition conditions of linked states

#### StateTransition Class
- Class used to call condition functions
- Condition functions require `TransitionFunction` marker in order to be identified by the state transition class

#### TransitionFunctionAttribute Class
- Class used for marking condition functions with `TransitionFunction` marker for `StateTransition` class to be able to call said functions

### Overview of Sample States
- Every state inherits from `AIState` class and overrides the following functions:
    - `OnStart()`
    - `OnUpdate()`
    - `OnExit()`
- Each state class also implements its own condition function(s) that need to be met in order for an agent to traverse from one state to another

#### Idle State
- The agent stays idle until it traverses to another state
- Defined as starting state (no condition is defined to traverse back to this state)

#### Patrol State
- The agent moves from one patrol point to another based on the provided list of patrol points (See `AgentPrefs` class)
- Agent will enter this state if target is out of the detection range value specified in `AgentPrefs` class.

#### Chase State
- The agent will continuously chase the target
- The state triggers when the target is within the detection range and outside of the attack range; both of these variables are specified in `AgentPrefs` class

#### Attack State
- The agent will shoot projectiles at the target, with applied cooldown
- The attack state occurs when the target is within the attack range

### Workflow
- Create a script that manages character attributes (i.e. character preferences)
- Add preferences script and `AIStateMachine` to your character
- Create states and define transitions list for each state; [See how to create custom states](#defining-custom-states)
- Add the initial state to the state machine component attached to the character

---

## Defining Custom States
- Custom states must inherit from `AIState` class and define `OnStart()`, `OnUpdate()`, and `OnExit()` functions.
- When defining your own states, make sure you mark condition functions with `[TransitionFunction]`
- The custom state class is outlined below:

```
using UnityEngine;

[CreateAssetMenu(fileName="your new state", menuName="ApexAI/Custom/Your State", order = 1)]
public class YourStateName : AIState
{
    // OnEnter is called when the agent enters the state
    public override void OnEnter(GameObject owner)
    {
        // Define enter behaviour of the state here
        Debug.Log($"{owner.name} entered your state.");
    }

    // OnExit is called when the agent is about to exit the state
    public override void OnExit(GameObject owner)
    {
        // Define exit behaviour of the state here
        Debug.Log($"{owner.name} exited your state.");
    }
    
    // OnUpdate is the body function of the state
    public override void OnUpdate(GameObject owner)
    {
        // Define continuous behaviour of the agent here
    }

    /*
    Condition functions are marked with [TransitionFunction]. They need to return a boolean value of true in
    order for the agent to enter the state. Condition functions can be named to whatever you like!
    */
    [TransitionFunction]
    public bool ConditionFunction(GameObject owner){
        Debug.Log("Condition function called!");
        return false;
    }

    /*
    If necessary, there can be more condition functions defined (e.g. conditions depend on what the previous
    state was the agent in)
    */
    [TransitionFunction]
    public bool AnotherConditionFunction(GameObject owner){
        Debug.Log("Another Condition function called!");
        return false;
    }
}
```

---

[Back to top](#apex-ai-documentation)