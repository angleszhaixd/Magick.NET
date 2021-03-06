﻿// Copyright 2013-2018 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   https://www.imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
// either express or implied. See the License for the specific language governing permissions
// and limitations under the License.

using System.IO;
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests.Shared
{
    public partial class MagickImageTests
    {
        [TestClass]
        public class TheWriteMethod
        {
            [TestMethod]
            public void ShouldUseTheFileExtension()
            {
                var readSettings = new MagickReadSettings()
                {
                    Format = MagickFormat.Png,
                };

                using (IMagickImage input = new MagickImage(Files.CirclePNG, readSettings))
                {
                    using (var tempFile = new TemporaryFile(".jpg"))
                    {
                        input.Write(tempFile);

                        using (IMagickImage output = new MagickImage(tempFile))
                        {
                            Assert.AreEqual(MagickFormat.Jpeg, output.Format);
                        }
                    }
                }
            }

            [TestMethod]
            public void ShouldUseTheSpecifiedFormat()
            {
                using (IMagickImage input = new MagickImage(Files.CirclePNG))
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        using (var stream = new NonSeekableStream(memoryStream))
                        {
                            input.Write(stream, MagickFormat.Tiff);

                            memoryStream.Position = 0;
                            using (IMagickImage output = new MagickImage(stream))
                            {
                                Assert.AreEqual(MagickFormat.Tiff, output.Format);
                            }
                        }
                    }
                }
            }
        }
    }
}
