﻿using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayerInterfaces
{
    public interface IAgentAccessor
    {
        List<Agents> SelectAgentsByStatus(bool active);
    }
}
