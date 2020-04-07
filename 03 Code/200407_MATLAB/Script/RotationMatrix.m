clear;
clc;
%% parameters

 al = (10*pi)/180;
 be = (20*pi)/180;
 ga = (30*pi)/180;

%syms al be ga
%% Rotation Matrix
Rotate_x = [1 0 0
            0 cos(al) -sin(al)
            0 sin(al) cos(al)];
Rotate_y = [cos(be) 0 sin(be)
            0 1 0
            -sin(be) 0 cos(be)];
Rotate_z = [cos(ga) -sin(ga) 0
            sin(ga) cos(ga) 0
            0 0 1];
        
        
bryant = Rotate_x*Rotate_y*Rotate_z;
euler  = Rotate_z*Rotate_y*Rotate_x;