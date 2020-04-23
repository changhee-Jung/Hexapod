clear;
%% parameters
Ph_b = 15 * pi/ 180; %% Base Offset
Ph_p = 10 * pi/ 180; %% Upper Offset
Rb   = 0.16; %% Base Radius
Rp   = 0.08; %% Upper Radius
h    = 0.16; %% Heights
%%T_Offset = [0 0.01 0.01]';
T_Offset = [0 0 0]';
%% Calculate Compensation
al = (10*pi)/180;
be = (20*pi)/180;
ga = (30*pi)/180;

Rm = [cos(be)*cos(ga) sin(al)*sin(be)*cos(ga)-cos(al)*sin(ga) cos(al)*sin(be)*cos(ga)+sin(al)*sin(ga);
      cos(be)*sin(ga) sin(al)*sin(be)*sin(ga)+cos(al)*cos(ga) cos(al)*sin(be)*sin(ga)-sin(al)*cos(ga);
      -sin(be) sin(al)*cos(be) cos(al)*cos(be)];
O_mod = Rm*[0 0 h]';
O_com = [O_mod(1) O_mod(2) (O_mod(3)-h)]'; %% P parameter contains h
%O_com = -[0 0 h]';
T_com = Rm*T_Offset;

Xpe = [0 0 0]';
Xp  = Xpe - O_com - T_com;

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


%% figure Drawing in Base & Upper
figure(1);
hold on;
axis([-0.3 0.3 -0.3 0.3]);
grid on;

plot(B1x,B1y,'o-');
plot(B2x,B2y,'o-');
plot(B3x,B3y,'o-');
plot(B4x,B4y,'o-');
plot(B5x,B5y,'o-');
plot(B6x,B6y,'o-');

plot(P1x,P1y,'+');
plot(P2x,P2y,'+');
plot(P3x,P3y,'+');
plot(P4x,P4y,'+');
plot(P5x,P5y,'+');
plot(P6x,P6y,'+');

%% Final coordinates of the hinges(Base, Upper)
B = [B1x B2x B3x B4x B5x B6x
     B1y B2y B3y B4y B5y B6y
     0 0 0 0 0 0];
 
P = [P1x P2x P3x P4x P5x P6x
     P1y P2y P3y P4y P5y P6y
     h h h h h h]; % when calculating, h value is compansated by O_com

%% Target Position of the P Vector
PP1 = Rm*P(:,1) + Xp;
PP2 = Rm*P(:,2) + Xp;
PP3 = Rm*P(:,3) + Xp;
PP4 = Rm*P(:,4) + Xp;
PP5 = Rm*P(:,5) + Xp;
PP6 = Rm*P(:,6) + Xp;

%% length Vector
S1 = PP1 - B(:,1);
S2 = PP2 - B(:,2);
S3 = PP3 - B(:,3);
S4 = PP4 - B(:,4);
S5 = PP5 - B(:,5);
S6 = PP6 - B(:,6);

%% Target length of the each Actuator
L1 = norm(S1);
L2 = norm(S2);
L3 = norm(S3);
L4 = norm(S4);
L5 = norm(S5);
L6 = norm(S6);
