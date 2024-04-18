---
tags:
 - Atmosphere
 - Feature
 - Implemented-Incomplete
---

Roadmap:
1. Functioning Atmosphere that changes dynamically
2. Pipes that connect different portions of the ship that otherwise wouldn't be connected.
    - Much like the wires and power, it doesn't have to be realistic. This would be easier to make realistic though, since most of the math is already taken care of.
3. Different Gasses that interact dynamically.

New Idea for computing atmospherics:
    Use a shader to compute the atmoshperic calculations instead of the normal gameloop. This idea came from this (https://www.youtube.com/watch?v=bqtqltqcQhw) video by Sebastian Lague in his "Coding Adventures" series specifically involving Boids (bird-like objects). In it, he found that he was having performance issues when iterating through each bird for each individual bird telling them to avoid one another by not getting too close. To resolve this, he used a shader to do the calculations, thereby putting the burden of processing onto the gpu rather than the cpu. His code is at 7:46 and is as follows:

[numthreads(threadGroupSize,1,1)]
void CSMain(uint3 id : SV_DispatchThreadID)
{
    for(int indexB = 0; indexB < numBoids; indexB++)
    {
        if(id.x != indexB)
        {
            Boid boidB = boids[indexB];
            float3 offset = boidB.position - boids[id.x].position;
            float sqrDst = offset.x * offset.x + offset.y * offset.y + offset * z + offset * z; //I think this is supposed to be the square root of all that, since it's supposed to be finding the distance, but this is as he wrote it.

            if(sqrDst < viewRadius * view Radius){
                boids[id.x].numFlockmates += 1;
                boids[id.x].flockHeading += boidB.direction;
                boids[id.x].flockCenter += boids.position;

                if(sqrDst < avoidRadius * avoidRadius){
                    boids[id.x].separationHeading -= offset/sqrDst;
                }
            }
        }
    }
}