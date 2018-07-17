using System;
using static SDL2.SDL;
using static SDL2.SDL.SDL_EventType;
using static SDL2.SDL.SDL_GLattr;
using static SDL2.SDL.SDL_Keycode;
using static SDL2.SDL.SDL_WindowFlags;

using static GLDotNet.GL;

namespace Sandbox
{
    class Program
    {
        public static int Main(string[] args)
        {
            if (SDL_Init(SDL_INIT_VIDEO) < 0)
            {
                return 1;
            }

            string title = "Hello SDL2";

            IntPtr window = SDL_CreateWindow(
                title,
                SDL_WINDOWPOS_UNDEFINED,
                SDL_WINDOWPOS_UNDEFINED,
                800,
                600,
                SDL_WINDOW_OPENGL | SDL_WINDOW_SHOWN);

            if (window == IntPtr.Zero)
            {
                return 1;
            }

            SDL_GL_SetAttribute(SDL_GL_CONTEXT_MAJOR_VERSION, 4);
            SDL_GL_SetAttribute(SDL_GL_CONTEXT_MINOR_VERSION, 0);
            SDL_GL_SetAttribute(SDL_GL_CONTEXT_PROFILE_MASK, 0x0001);

            IntPtr glContext = SDL_GL_CreateContext(window);

            if (glContext == IntPtr.Zero)
            {
                return 1;
            }

            glInit(SDL_GL_GetProcAddress, 4, 0);
            glClearColor(1, 0, 0, 1);

            bool quit = false;

            SDL_Event e;
            while (!quit)
            {
                while (SDL_PollEvent(out e) != 0)
                {
                    switch (e.type)
                    {
                        case SDL_KEYDOWN:
                            if (e.key.keysym.sym == SDLK_ESCAPE)
                            {
                                quit = true;
                            }
                            else
                            {
                                SDL_SetWindowTitle(window, "KeyDown: " + e.key.keysym.sym);
                            }
                            break;

                        case SDL_QUIT:
                            quit = true;
                            break;
                    }
                }

                glClear(GL_COLOR_BUFFER_BIT);

                SDL_GL_SwapWindow(window);
            }

            SDL_DestroyWindow(window);
            SDL_Quit();
            return 0;
        }
    }
}
