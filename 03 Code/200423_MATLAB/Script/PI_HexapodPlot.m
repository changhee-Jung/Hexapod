clear all;
clc

%% parameters
Ph_b = 18 * pi/ 180; %% Base Offset
Ph_p = 18 * pi/ 180; %% Upper Offset
Rb   = 68; %% Base Radius [mm]
Rp   = 50; %% Upper Radius [mm]
h    = 88.3; %% Heights [mm]
%%T_Offset = [0 0.01 0.01]';
T_Offset = [0 0 0]';

%% Angles of 12 hinges(Base, Upper)
Pb1 = -pi/3 + Ph_b;
Pb2 = -pi/3 - Ph_b;
Pb3 = -pi + Ph_b;
Pb4 = -pi - Ph_b;
Pb5 = -5*pi/3 + Ph_b;
Pb6 = -5*pi/3 - Ph_b;

Pp1 = -Ph_p;
Pp2 = -2*pi/3 + Ph_p;
Pp3 = -2*pi/3 - Ph_p;
Pp4 = -4*pi/3 + Ph_p;
Pp5 = -4*pi/3 - Ph_p;
Pp6 = -2*pi + Ph_p;

%% Coordinate of 12 hinges(Base, Upper)
B1x = Rb*cos(Pb1);
B1y = Rb*sin(Pb1);

B2x = Rb*cos(Pb2);
B2y = Rb*sin(Pb2);

B3x = Rb*cos(Pb3);
B3y = Rb*sin(Pb3);

B4x = Rb*cos(Pb4);
B4y = Rb*sin(Pb4);

B5x = Rb*cos(Pb5);
B5y = Rb*sin(Pb5);

B6x = Rb*cos(Pb6);
B6y = Rb*sin(Pb6);

P1x = Rp*cos(Pp1);
P1y = Rp*sin(Pp1);

P2x = Rp*cos(Pp2);
P2y = Rp*sin(Pp2);

P3x = Rp*cos(Pp3);
P3y = Rp*sin(Pp3);

P4x = Rp*cos(Pp4);
P4y = Rp*sin(Pp4);

P5x = Rp*cos(Pp5);
P5y = Rp*sin(Pp5);

P6x = Rp*cos(Pp6);
P6y = Rp*sin(Pp6);


B = {B1x B1y 0;
     B2x B2y 0;
     B3x B3y 0;
     B4x B4y 0;
     B5x B5y 0;
     B6x B6y 0};
 
P = {P1x P1y 0;
     P2x P2y 0;
     P3x P3y 0;
     P4x P4y 0;
     P5x P5y 0;
     P6x P6y 0};
 
 
% (0,0,0,0,0,0) 일때의 각 Vector 값
Vector = {-2.98102231770514	30.050031513655	88.3;
-24.5335795164956	17.6066568132085	88.3;
27.5146018342007	-12.4433747004465	88.3;
27.5146018342007	12.4433747004465	88.3;
-24.5335795164956	-17.6066568132085	88.3;
-2.9810223177051	-30.050031513655	88.3};

%% figure Drawing in Base & Upper
figure(2);
clf('reset');
hold on;
%%axis([-0.3 0.3 -0.3 0.3]);
grid on;

quiver3(B1x,B1y,0,cell2mat(Vector(1,1)),cell2mat(Vector(1,2)),cell2mat(Vector(1,3)));
hold on
quiver3(B2x,B2y,0,cell2mat(Vector(2,1)),cell2mat(Vector(2,2)),cell2mat(Vector(2,3)));
quiver3(B3x,B3y,0,cell2mat(Vector(3,1)),cell2mat(Vector(3,2)),cell2mat(Vector(3,3)));
quiver3(B4x,B4y,0,cell2mat(Vector(4,1)),cell2mat(Vector(4,2)),cell2mat(Vector(4,3)));
quiver3(B5x,B5y,0,cell2mat(Vector(5,1)),cell2mat(Vector(5,2)),cell2mat(Vector(5,3)));
quiver3(B6x,B6y,0,cell2mat(Vector(6,1)),cell2mat(Vector(6,2)),cell2mat(Vector(6,3)));
t = linspace(0,2*pi,10000);
BCircle_x = Rb*cos(t);
BCircle_y = Rb*sin(t);
BCircle_z = linspace(0,0,10000);
PCircle_x = Rp*cos(t);
PCircle_y = Rp*sin(t);
PCircle_z = linspace(h,h,10000);

plot3(BCircle_x,BCircle_y,BCircle_z);
plot3(PCircle_x,PCircle_y,PCircle_z);

