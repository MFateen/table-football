# table-football
A team project for the Machine Intelligence module in Cairo University Faculty of Engineering

## Problem Description
It is required to design and implement an intelligent agent that will play a simple version of a table
football (foosball) playing game that is described in the next section. The agent will play against other
agents implemented by other project groups. Communication rules are to be set between groups' leaders
and co-leaders in a signed-agreement that all teams have to follow.

## Environment
In the simplified game version for this project, there are two teams Red and Blue. Each foosball
team has only two rods (with six players). One rod is for the defense (with three players with one square
apart). The other rod is for the offense, also with three players with one square apart placed as shown in
Figure 1. Red team goal area and defense rod are on the left part of the playground, the offense rod is on
the right part and vice versa for the blue team. For simplicity, the play ground is modeled by a 7 *11
squares board in addition to two rectangles of size 3 squares, each one outside each side of the
playground representing the goal as shown in Figure 1.
Each agent is in a normal active state and can simultaneously move one or two of his rods and can
choose independently one of three possible actions for each rod RIGHT, LEFT, KICK. RIGHT and LEFT
actions move all the players on the same rod in the same direction one square to the right or to the left
respectively (the direction is referenced normally w.r.t. the side who originated the move, so the red team right move will be the opposite direction of the blue team right move). KICK action has a choice of moving
the ball one to maximum five squares in three possible directions with respect to the player on the rod
that had the ball in its reach straight to a square in the same direction or to the left or right diagonal. The
reach of a player is one square straight infront or behind in addition to its own square. A NO-ACTION
state is applied to any rod to keep players of a rod in the same square with normal active state. When a
ball comes to a square that has a NO-ACTION state player it is simply assumed that the ball will rebound
two squares straight in the opposite direction the ball was thrown. If the ball reaches any of the three
squares in front of the goal and the square had no opponent agent the ball will continue its way onto the
goal area and a goal is scored. If an opponent exists then he owns the ball and starts playing. The ball is
not allowed to go out nor be dead at any time, this means that any position can be reached by at least
one of the players in the two teams. If it is not reachable by “Red” team, it will be reachable by “Blue”
team. If the ball reaches the border, it is assumed that it will just stop at the last square at the border.
The ball position at the start of the game is in the middle square as shown in Figure 1 with “Red”
team starting and owning the ball. If the ball goes to the conflict middle line during the match, the “Red”
agent always takes the ball. Timed communication messages exchanged between the agents should be
logged in a file at both ends with the same format decided between the teams in a signed agreement.
This will act as a state of proof if any of the teams claim that their agent sends messages that are not
received by the other team.
A match will last for five live minutes. Both agent and opponent actions are to be rendered/shown on the
GUI of both agents. A timer clock will be set live in the room during playing and at the buzz the winning
team will be that with the larger number of goals. The competition has 5 teams, each team will play two
matches with the rest of the teams so that in one match the agent is “Red” team and in the other “Blue”. It
is the responsibility of each team to record the results in a file indicating the score achieved in each match
played.

## Group Memebers and Job description
* Mostafa Fateen ([MFateen] (https://github.com/MFateen))
* Abdallah Sobehy ([Abdallah-Sobehy] (https://github.com/Abdallah-Sobehy))

### tobe completed
