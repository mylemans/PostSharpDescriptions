PostSharpDescriptions v1.0
     (c) 2011 Tim Mylemans 
--------------------------

PostSharpDescriptions allows you to add one or more descriptions to elements that are manipulated by an aspect. 
This is done at post-compile (when the aspect is applied to the assembly), and in the on-hover pop up, as an addition
to the information that is shown there already.

Features
--------

*	Can currently target any of the following elements: Field / Property / Method / Type
*	Can target getter/setter of properties, if you supply both the property and the getter/setter method

Installation
------------

*	Modify your PostSharp.targets file. If you forget this, a warning will be generated @ compilation.
	Find: Properties="$(PostSharpProperties)"
	Replace with: Properties="$(PostSharpProperties);TargetName=$(TargetName);OutputPath=$(OutputPath)" 
*	You could also add the missing properties per-project, or use a PostSharp.Custom.targets

Usage
-----

*	Reference the PostSharpDescriptions library (as far as I know, you won't need the library after compilation, so put "Copy Local" to false)
*	Use the PostSharpDescription class directly: PostSharpDescription.Add<TAspect>(description, ...)
*	Use the extension methods (extending @ IAspect): this.Describe(description, ...)

Known issues
------------

*	The source aspect must apply on the target already, or the message will not be shown. 
	For example you cannot target a Type from an OnMethodBoundary aspect.
*	Applying the same aspect on different levels, will result in the description only being added to one of them.
	The description will still be shown, but they will all be shown on the same 'enhanced' bit of the pop up.
	Example: Applying an OnMethodBoundary aspect on both the declaring type and a method, 
	will on-hover show the added descriptions as if they were both added by the one that was applied at type level. 
	I couldn't find a way to detect at compile time that an aspect was applied to the declaring type, or just at the method.
*	Will collide if there are 1000000+ entries in the pssym file, as our added entries start counting from 1000000 (you could increase it though)

Remarks
-------

*	PostSharpDescriptions adds in a nice (partial) solution for not being able to add text to the on-hover pop up that PostSharp shows.
	This is done in the hope that eventually such a feature will be added in a future release of PostSharp as now you know what you're missing out on.
	The ability to generate extra descriptions that are shown on-hover make it so much easier to maintain overview of what an aspect does.
*	It is free, and not perfect. Be free to make improvements, but you wont be able (as far as I know that is) to finish the 2nd issue from the 'Known Issues' list.
*	Hope you find good use for this.
*	It is MIT licensed, don't think you can complain about that one.
*	Check out the PostSharpDescriptions.Test project to see it in action. Will also show the known issue #2 @ BasicTestClass::Method()
*	Make sure you modified your PostSharp.targets or PostSharpDescriptions will not do anything.

License
-------

Copyright (C) 2011 by Tim Mylemans <tim@mylemans.com>

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
