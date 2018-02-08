﻿using System;
using System.Collections.Generic;
using System.Text;
using Dubbo.Net.Common;

namespace Dubbo.Net.Remoting.Transport
{
    public abstract class AbstractEndpoint : AbstractPeer, IResetable
    {
        private static readonly ILogger logger;//todo get logger from container

        protected ICodec Codec { get; set; }

        protected int Timeout { get; set; }

        protected int ConnectTimeout { get; set; }

        public AbstractEndpoint(URL url, IChannelHandler handler) : base(url, handler)
        {
            this.Codec = GetChannelCodec(url);
            this.Timeout = url.GetPositiveParameter(Constants.TimeoutKey, Constants.DefaultTimeout);
            this.ConnectTimeout = url.GetPositiveParameter(Constants.ConnectTimeoutKey, Constants.DefaultConnectTimeout);
        }

        protected static ICodec GetChannelCodec(URL url)
        {
            String codecName = url.GetParameter(Constants.CodecKey, "telnet");
            return null;
            //todo get codec from container
        }

        public void Reset(URL url)
        {
            if (IsClosed)
            {
                throw new Exception("Failed to reset parameters "
                        + url + ", cause: Channel closed. channel: " + Address);
            }
            try
            {
                if (url.HasParameter(Constants.TimeoutKey))
                {
                    int t = url.GetParameter(Constants.TimeoutKey, 0);
                    if (t > 0)
                    {
                        this.Timeout = t;
                    }
                }
            }
            catch (Exception t)
            {
                logger.Error( t);
            }
            try
            {
                if (url.HasParameter(Constants.ConnectTimeoutKey))
                {
                    int t = url.GetParameter(Constants.ConnectTimeoutKey, 0);
                    if (t > 0)
                    {
                        this.ConnectTimeout = t;
                    }
                }
            }
            catch (Exception t)
            {
                logger.Error( t);
            }
            try
            {
                if (url.HasParameter(Constants.CodecKey))
                {
                    this.Codec = GetChannelCodec(url);
                }
            }
            catch (Exception t)
            {
                logger.Error(t);
            }
        }





    }
}