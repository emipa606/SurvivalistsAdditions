using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Verse;

namespace SurvivalistsAdditions;

public class PatchOperationModDependent : PatchOperation
{
    private string modName;
    private List<string> optionalModsList;

    private List<string> requiredModsList;


    protected override bool ApplyWorker(XmlDocument xml)
    {
        if (!requiredModsList.NullOrEmpty())
        {
            return ApplyWorker_Multiple();
        }

        if (!optionalModsList.NullOrEmpty())
        {
            return ApplyWorker_GetAny();
        }

        return !modName.NullOrEmpty() && ApplyWorker_Single();
    }


    private bool ApplyWorker_Multiple()
    {
        foreach (var requiredMod in requiredModsList)
        {
            if (ModsConfig.ActiveModsInLoadOrder.All(mod => mod.Name != requiredMod))
            {
                return false;
            }
        }

        return true;
    }


    private bool ApplyWorker_GetAny()
    {
        foreach (var optionalMod in optionalModsList)
        {
            if (ModsConfig.ActiveModsInLoadOrder.Any(mod => mod.Name == optionalMod))
            {
                return true;
            }
        }

        return false;
    }


    private bool ApplyWorker_Single()
    {
        return ModsConfig.ActiveModsInLoadOrder.Any(mod => mod.Name == modName);
    }
}