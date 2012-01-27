﻿//====================================================================================================================
// Copyright (c) 2012 IdeaBlade
//====================================================================================================================
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE 
// WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS 
// OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR 
// OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE. 
//====================================================================================================================
// USE OF THIS SOFTWARE IS GOVERENED BY THE LICENSING TERMS WHICH CAN BE FOUND AT
// http://cocktail.ideablade.com/licensing
//====================================================================================================================

using System.Collections.Generic;
using IdeaBlade.Core.Composition;

namespace Cocktail.Tests.Helpers
{
    public class CompositionContextResolver : ICompositionContextResolver
    {
        private static readonly Dictionary<string, CompositionContext> CompositionContexts =
            new Dictionary<string, CompositionContext>();

        public CompositionContext GetCompositionContext(string compositionContextName)
        {
            if (!CompositionContexts.ContainsKey(compositionContextName)) 
                return null;

            return CompositionContexts[compositionContextName];
        }

        public static void Add(CompositionContext compositionContext)
        {
            if (CompositionContexts.ContainsKey(compositionContext.Name))
                CompositionContexts.Remove(compositionContext.Name);

            CompositionContexts.Add(compositionContext.Name, compositionContext);
        }
    }
}