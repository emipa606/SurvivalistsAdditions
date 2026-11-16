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
        return requiredModsList.All(requiredMod =>
            ModsConfig.ActiveModsInLoadOrder.Any(mod => mod.Name == requiredMod));
    }


    private bool ApplyWorker_GetAny()
    {
        return optionalModsList.Any(optionalMod =>
            ModsConfig.ActiveModsInLoadOrder.Any(mod => mod.Name == optionalMod));
    }

    private bool ApplyWorker_Single()
    {
        return ModsConfig.ActiveModsInLoadOrder.Any(mod => mod.Name == modName);
    }
}